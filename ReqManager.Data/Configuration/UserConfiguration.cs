using ReqManager.Model.Models;
using System.Data.Entity.ModelConfiguration;

namespace ReqManager.Data.Configuration
{
    public class UserConfiguration : EntityTypeConfiguration<UserModel>
    {
        public UserConfiguration()
        {
            ToTable("User");
            HasKey(u => u.ID_user);
            Property(c => c.login).IsRequired().HasMaxLength(50);
            Property(c => c.senha).IsRequired().HasMaxLength(50);
            Property(c => c.nome).IsRequired().HasMaxLength(50);
            
        }
    }
}
