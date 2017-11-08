using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Acess
{
    public class UserEntity
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        [Display(Name = "Full Name")]
        public string name { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(10)]
        [Display(Name = "Nick Name")]
        public string nickName { get; set; }
        [MinLength(10)]
        [MaxLength(1000)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "E-mail")]
        public string email { get; set; }
        [Required]
        [MaxLength(15)]
        [MinLength(6)]
        [Display(Name = "Login")]
        public string login { get; set; }
        [Required]
        [Display(Name = "B-Day")]
        public System.DateTime dateOfBirth { get; set; }
        [Required]
        [MaxLength(30)]
        [Display(Name = "Profession")]
        public string profession { get; set; }
        [Required]
        [MaxLength(20)]
        [Display(Name = "Document")]
        public string document { get; set; }
        [Required]
        [Display(Name = "Active")]
        public Boolean active { get; set; }

        public String DisplayName
        {
            get
            {
                return nickName + " - " + login;
            }
        }
    }
}
