using ReqManager.Entities.Acess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Link
{
    public class TypeLinkEntity
    {
        [Key]
        public int TypeLinkID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        [MaxLength(50), MinLength(3)]
        public string description { get; set; }
        [Required]
        public DateTime creationDate { get; set; }

        public virtual UserEntity Users { get; set; }
    }
}
