using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories;
using ReqManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Services.Acess
{
    public interface IUserService
    {
        IEnumerable<User> Get(string name = null);
        User GetById(int id);
        void Create(User user);
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

        public void Create(User user)
        {
            repository.Add(user);
        }

        public User GetById(int id)
        {
            return repository.GetById(id);
        }

        public IEnumerable<User> Get(string login = null)
        {
            return string.IsNullOrEmpty(login) ? repository.GetAll() : repository.GetAll().Where(u => u.login.Equals(login));
        }

        public void Save()
        {
            unit.Commit();
        }
    }
}
