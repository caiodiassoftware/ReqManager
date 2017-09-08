using ReqManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Services.InterfacesServices
{
    public interface IRoleService
    {
        IEnumerable<Role> filterByRole(string roleName);
        IEnumerable<Role> GetAll();
        Role Get(int? id);
        void saveChanges();
        void add(Role userRole);
        void edit(Role userRole);
        void delete(Role userRole);
    }
}
