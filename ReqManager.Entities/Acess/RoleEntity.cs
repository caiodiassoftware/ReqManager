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
        [Display(Name = "Role Name")]
        public string name { get; set; }
        [MinLength(5)]
        [MaxLength(255)]
        [Display(Name = "Role Description")]
        public string description { get; set; }

    }
}
