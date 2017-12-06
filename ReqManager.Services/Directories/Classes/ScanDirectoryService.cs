using System;
using System.Collections.Generic;
using ReqManager.Services.Directories.Interfaces;
using System.IO;
using System.Linq;
using ReqManager.Utils.Extensions;
using System.Security.AccessControl;

namespace ReqManager.Services.Directories.Classes
{
    public class ScanDirectoryService : IScanDirectoryService
    {
        public IEnumerable<string> getAllDirectoriesInPath(string path)
        {
            List<string> allDirectories = Directory.GetDirectories(path, "*.*", SearchOption.AllDirectories).ToList();
            return allDirectories;
        }

        private IEnumerable<string> getAllFilesInPath(string path)
        {
            return System.IO.Directory.GetFiles(path, "*.*", System.IO.SearchOption.AllDirectories).ToList();
        }

        public IEnumerable<string> getFolders(string path)
        {
            return Directory.EnumerateFileSystemEntries(path);
        }

        public List<string> findFile(string[] values, string path)
        {
            try
            {
                List<string> filesFound = new List<string>();

                if (Directory.Exists(path))
                {
                    var files = getAllFilesInPath(path);

                    foreach (string file in files)
                    {
                        if(CanRead(file))
                        {
                            FileInfo info = new FileInfo(file);
                            using (StreamReader sr = info.OpenText())
                            {
                                string s = string.Empty;
                                while ((s = sr.ReadLine()) != null)
                                    if (s.ContainsAny(values))
                                        filesFound.Add(file);
                                sr.Close();
                            }
                        }
                    }
                }
                else
                {
                    filesFound.Add("Path not Found");
                }

                return filesFound;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool CanRead(string path)
        {
            var readAllow = false;
            var readDeny = false;
            var accessControlList = Directory.GetAccessControl(path);
            if (accessControlList == null)
                return false;
            var accessRules = accessControlList.GetAccessRules(true, true, typeof(System.Security.Principal.SecurityIdentifier));
            if (accessRules == null)
                return false;

            foreach (FileSystemAccessRule rule in accessRules)
            {
                if ((FileSystemRights.Read & rule.FileSystemRights) != FileSystemRights.Read) continue;

                if (rule.AccessControlType == AccessControlType.Allow)
                    readAllow = true;
                else if (rule.AccessControlType == AccessControlType.Deny)
                    readDeny = true;
            }

            return readAllow && !readDeny;
        }
    }
}
