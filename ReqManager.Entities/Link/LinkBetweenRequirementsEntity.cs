using ReqManager.Entities.Acess;
using ReqManager.Entities.Requirement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Link
{
    public class LinkBetweenRequirementsEntity
    {
        [Key]
        public int LinkRequirementsID { get; set; }
        [Required]
        [Display(Name = "User")]
        public int UserID { get; set; }
        [Required]
        [Display(Name = "Type Link")]
        public int TypeLinkID { get; set; }
        [Required]
        [Display(Name = "Creation Data")]
        public DateTime creationDate { get; set; }
        [Required]
        [MaxLength(25), MinLength(5)]
        [Display(Name = "Code")]
        public string code { get; set; }

        public String DisplayName
        {
            get
            {
                return this.RequirementOriginID.code + " to " + this.RequirementTargetID.code + " - " + this.TypeLink.description;
            }
        }

        public virtual UserEntity Users { get; set; }
        public virtual RequirementEntity RequirementOriginID { get; set; }
        public virtual RequirementEntity RequirementTargetID { get; set; }
        public virtual TypeLinkEntity TypeLink { get; set; }
    }
}
