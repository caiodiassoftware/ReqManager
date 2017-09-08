using ReqManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Services.InterfacesServices
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User filterByLogin(string login);
        User Get(int? id);
        void saveChanges();
        void add(User userRole);
        void edit(User userRole);
        void delete(User userRole);
    }
}
