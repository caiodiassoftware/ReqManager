using ReqManager.Data.Repositories;
using ReqManager.Model.Models;
using ReqManager.Services;
using Store.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Services
{
    public interface IUserService
    {
        IEnumerable<UserModel> Get(string name = null);
        UserModel GetById(int id);
        void Create(UserModel user);
        void Save();
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository repository;
        private readonly IUnitOfWork unit;

        public UserService(IUserRepository repository, IUnitOfWork unit)
        {
            this.repository = repository;
            this.unit = unit;
        }

        public void Create(UserModel user)
        {
            repository.Add(user);
        }

        public UserModel GetById(int id)
        {
            return repository.GetById(id);
        }

        public IEnumerable<UserModel> Get(string login = null)
        {
            return string.IsNullOrEmpty(login) ? repository.GetAll() : repository.GetAll().Where(u => u.login.Equals(login));
        }

        public void Save()
        {
            unit.Commit();
        }
    }

}
