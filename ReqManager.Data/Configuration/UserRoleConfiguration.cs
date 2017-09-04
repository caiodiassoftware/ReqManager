using ReqManager.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Data.Configuration
{
    public class UserRoleConfiguration : EntityTypeConfiguration<UserRoleModel>
    {
        public UserRoleConfiguration()
        {
            ToTable("UserRole");
            HasKey(u => u.ID_userRole);
            
        }
    }
}
