using ReqManager.Entities.Acess;
using System;
using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Link
{
    public class TypeLinkEntity
    {
        [Key]
        [Display(Name = "Type Link")]
        public int TypeLinkID { get; set; }
        [Required]
        [Display(Name = "User")]
        public int CreationUserID { get; set; }
        [Required]
        [MaxLength(50), MinLength(3)]
        [Display(Name = "Type Link Description")]
        [DataType(DataType.MultilineText)]
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
