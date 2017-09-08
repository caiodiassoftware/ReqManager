using ReqManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Services.InterfacesServices
{
    public interface IRoleControllerActionService
    {
        IEnumerable<RoleControllerAction> GetAll();
        IEnumerable<RoleControllerAction> filterByRole(string roleName);
        RoleControllerAction Get(int? id);
        void saveChanges();
        void add(RoleControllerAction userRole);
        void edit(RoleControllerAction userRole);
        void delete(RoleControllerAction userRole);
    }
}
