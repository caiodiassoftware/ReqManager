using System;
using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Acess
{
    public class UserRoleEntity
    {
        [Key]
        [Display(Name = "User/Role")]
        public int UserRoleID { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        [Range(1, Double.PositiveInfinity)]
        [Display(Name = "Role")]
        public int RoleID { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        [Display(Name = "User Name")]
        public int UserID { get; set; }

        public String DisplayName
        {
            get
            {
                return this.User.nickName + " - " + this.Role.description;
            }
        }

        public virtual RoleEntity Role { get; set; }
        public virtual UserEntity User { get; set; }

    }
}
