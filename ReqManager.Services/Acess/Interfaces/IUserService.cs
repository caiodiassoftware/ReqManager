using ReqManager.Services.Estructure;
using System;
using ReqManager.Entities.Acess;

namespace ReqManager.Services.Acess.Interfaces
{
    public interface IUserService : IService<UserEntity>
    {
        UserEntity Login(String login, String senha);
        UserEntity Get(String login);
    }
}
