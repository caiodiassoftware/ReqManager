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
        [Display(Name = "Description")]
        public string description { get; set; }
        [Required]
        [Display(Name = "Creation Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Invalid Date")]
        public DateTime creationDate { get; set; }

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
