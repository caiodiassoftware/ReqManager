using ReqManager.Services.Estructure;
using ReqManager.Data.Infrastructure;
using ReqManager.Services.Acess.Interfaces;
using ReqManager.Data.InterfacesRepositories;
using ReqManager.Model;
using System.Linq;
using ReqManager.Entities.Acess;
using System;
using ReqManager.Utils.Hashing;
using System.Web.Security;
using ReqManager.Services.Others.Interfaces;

namespace ReqManager.Services.Acess.Classes
{
    public class UserService : ServiceBase<Users, UserEntity>, IUserService
    {
        private IEmailService email { get; set; }

        public UserService(
            IUserRepository repository,
            IEmailService email,
            IUnitOfWork unit) : base(repository, unit)
        {
            this.email = email;
        }

        public override void add(ref UserEntity entity, bool persistir = true)
        {

            try
            {
                string randomPassword = Membership.GeneratePassword(6, 1);
                var keyNew = CryptographySHA1.GeneratePassword(10);
                var password = CryptographySHA1.EncodePassword(randomPassword, keyNew);
                entity.password = password;
                entity.verificationCode = keyNew;
                entity.active = true;
                base.add(ref entity, persistir);
                sendEmailUser(entity, randomPassword);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public UserEntity Get(string login)
        {
            try
            {
                Users user = repository.filter(u => u.login == login).FirstOrDefault();
                return convertModelToEntity(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserEntity Login(string login, string password)
        {
            try
            {
                Users user = repository.filter(u => u.login.Equals(login) && u.password.Equals(password) && u.active).SingleOrDefault();
                return convertModelToEntity(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ResetPassword(string login, string document, string newPassword)
        {
            try
            {
                Users userModel = repository.filter(u => u.login.Equals(login) &&
                u.document.Equals(document) && u.active).SingleOrDefault();
                UserEntity user = convertModelToEntity(userModel);

                if(user != null)
                {
                    var keyNew = CryptographySHA1.GeneratePassword(10);
                    var password = CryptographySHA1.EncodePassword(newPassword, keyNew);
                    user.password = password;
                    user.verificationCode = keyNew;
                    base.update(ref user);
                    sendEmailUser(user, newPassword);
                    return true;
                }

                throw new ArgumentException("Login or Document are Incorrent!");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void sendEmailUser(UserEntity user, string password)
        {
            try
            {
                email.addHeaderH1("Register in ReqManager Application");
                email.addParagraph("<b>Name</b>: " + user.name);
                email.addParagraph("<b>Login</b>: " + user.login);
                email.addParagraph("<b>Password</b>: " + password);
                email.send("ReqManager Access Data", user.email, user.name);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
