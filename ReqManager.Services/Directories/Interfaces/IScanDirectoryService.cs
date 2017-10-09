using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Services.Directories.Interfaces
{
    public interface IScanDirectoryService
    {
        IEnumerable<string> getFolders(string path);
        IEnumerable<string> getAllDirectoriesInPath(string path);
        List<string> findFile(string[] value, string path);
    }
}
