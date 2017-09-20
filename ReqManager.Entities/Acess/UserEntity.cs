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
        public string name { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(10)]
        public string nickName { get; set; }
        [Required]
        [MinLength(10)]
        [MaxLength(1000)]
        public string password { get; set; }
        [Required]
        [MaxLength(50)]
        public string email { get; set; }
        [Required]
        [MaxLength(15)]
        [MinLength(6)]
        public string login { get; set; }
        [Required]
        public System.DateTime dateOfBirth { get; set; }
        [Required]
        [MaxLength(30)]
        public string profession { get; set; }
        [Required]
        [MaxLength(20)]
        public string document { get; set; }
        [Required]
        public Boolean active { get; set; }
    }
}
