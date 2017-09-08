using ReqManager.Data.Infrastructure;
using ReqManager.Data.InterfacesRepositories;
using ReqManager.Data.Repositories;
using ReqManager.Models;
using ReqManager.Services.InterfacesServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Services.Acess
{
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

        public IEnumerable<User> GetAll()
        {
            return repository.GetAll();
        }

        public User filterByLogin(string login)
        {
            return repository.GetUserByLogin(login);
        }

        public User get(int id)
        {
            return repository.GetById(id);
        }

        public void saveChanges()
        {
            unit.Commit();
        }

        public User Get(int? id)
        {
            throw new NotImplementedException();
        }

        public void add(User userRole)
        {
            throw new NotImplementedException();
        }

        public void edit(User userRole)
        {
            throw new NotImplementedException();
        }

        public void delete(User userRole)
        {
            throw new NotImplementedException();
        }
    }
}
