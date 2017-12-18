using System.Collections.Generic;

namespace ReqManager.Services.Directories.Interfaces
{
    public interface IScanDirectoryService
    {
        IEnumerable<string> getFolders(string path);
        IEnumerable<string> getAllDirectoriesInPath(string path);
        List<string> findFile(string[] value, string path);
        List<string> findFile(string value, string path);
    }
}
