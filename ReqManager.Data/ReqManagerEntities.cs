using ReqManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Data
{
    public class ReqManagerEntities : DbContext
    {
        public ReqManagerEntities() : base("DefaultConnection")
        {

        }

        public IDbSet<User> Users { get; set; }
        public IDbSet<UserRole> UsersRoles { get; set; }
        public IDbSet<Role> Roles { get; set; }
        public IDbSet<RoleControllerAction> RoleControllerActions { get; set; }
        public IDbSet<ControllerAction> ControllerActions { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
