using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Models
{
    [Table("USER", Schema = "ACESS")]
    public class User
    {
        [Key]
        [ScaffoldColumn(false)]
        public int ID_user { get; set; }
        [Required]
        [MaxLength(50)]
        public string nome { get; set; }
        [Required]
        [MaxLength(50)]
        public string login { get; set; }
        [Required]
        [MaxLength(50)]
        public string email { get; set; }
        [Required]
        [MaxLength(50)]
        public string senha { get; set; }

        public virtual UserRole userRole { get; set; }

    }
}
