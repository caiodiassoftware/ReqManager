using ReqManager.Model;
using ReqManager.Services.Estructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReqManager.Entities.Acess;

namespace ReqManager.Services.Acess.Interfaces
{
    public interface IUserService : IService<UserEntity>
    {
        Users Login(String login, String senha);
    }
}
