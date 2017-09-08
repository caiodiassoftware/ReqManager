using ReqManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Services.InterfacesServices
{
    public interface IUserRoleService
    {
        IEnumerable<UserRole> GetAll();
        IEnumerable<UserRole> filterByRole(string roleName);
        IEnumerable<UserRole> filterByUser(string roleName);
        UserRole Get(int? id);
        void saveChanges();
        void add(UserRole userRole);
        void edit(UserRole userRole);
        void delete(UserRole userRole);
    }
}
