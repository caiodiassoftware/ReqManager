using ReqManager.Data.Configuration;
using ReqManager.Model.Models;
using System.Data.Entity;

namespace Store.Data
{
    public class StoreEntities : DbContext
    {
        public StoreEntities() : base("DefaultConnection")
        {

        }

        public IDbSet<UserModel> Users { get; set; }
        public IDbSet<UserRoleModel> UsersRoles { get; set; }
        public IDbSet<RoleModel> Roles { get; set; }
        public IDbSet<RoleControllerActionModel> RoleControllerActions { get; set; }
        public IDbSet<ControllerActionModel> ControllerActions { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new UserConfiguration());
        }
    }
}
