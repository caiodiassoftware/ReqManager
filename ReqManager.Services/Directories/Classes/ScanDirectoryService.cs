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
            try
            {
                return File.Exists(path) ? Directory.GetDirectories(path, "*.*", SearchOption.AllDirectories).ToList() : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private IEnumerable<string> getAllFilesInPath(string path)
        {
            try
            {
                return System.IO.Directory.GetFiles(path, "*.*", System.IO.SearchOption.AllDirectories).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<string> getFolders(string path)
        {
            try
            {
                return File.Exists(path) ? Directory.EnumerateFileSystemEntries(path) : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> findFile(string value, string path)
        {
            string[] values = { value };
            return findFile(values, path);
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
                        if (CanRead(file))
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
                    filesFound.Add(path);
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
            try
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
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
