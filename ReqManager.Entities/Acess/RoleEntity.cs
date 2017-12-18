using System;
using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Acess
{
    public class RoleEntity
    {
        [Key]
        [Display(Name = "Role")]
        public int RoleID { get; set; }
        [MinLength(5)]
        [MaxLength(50)]
        [Required]
        [Display(Name = "Role Name")]
        public string name { get; set; }
        [MinLength(5)]
        [MaxLength(255)]
        [Required]
        [Display(Name = "Role Description")]
        [DataType(DataType.MultilineText)]
        public string description { get; set; }

        public String DisplayName
        {
            get
            {
                return this.name + " - " + this.description;
            }
        }

    }
}
