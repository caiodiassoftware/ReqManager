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
        [Display(Name = "User")]
        public int UserID { get; set; }
        [Required]
        [MaxLength(50), MinLength(3)]
        [Display(Name = "Type Link Description")]
        public string description { get; set; }
        [Display(Name = "Creation Date")]
        public System.DateTime creationDate { get; set; } = DateTime.Now;

        public String DisplayName
        {
            get
            {
                return this.description;
            }
        }

        public virtual UserEntity Users { get; set; }
    }
}
