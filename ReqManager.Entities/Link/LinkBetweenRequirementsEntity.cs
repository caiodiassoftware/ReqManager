using ReqManager.Entities.Acess;
using ReqManager.Entities.Requirement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Display(Name = "Req. Origin")]
        public int RequirementOriginID { get; set; }
        [Required]
        [Display(Name = "Req. Target")]
        public int RequirementTargetID { get; set; }
        [Required]
        [Display(Name = "Creation Data")]
        public System.DateTime creationDate { get; set; }
        [Required]
        [MaxLength(25), MinLength(5)]
        [Display(Name = "Code")]
        public string code { get; set; }

        public String DisplayName
        {
            get
            {
                return this.RequirementOrigin.code + " to " + this.RequirementTarget.code + " using " + this.TypeLink.description;
            }
        }

        public virtual UserEntity Users { get; set; }
        [ForeignKey("RequirementOriginID")]
        public virtual RequirementEntity RequirementOrigin { get; set; }
        [ForeignKey("RequirementTargetID")]
        public virtual RequirementEntity RequirementTarget { get; set; }
        public virtual TypeLinkEntity TypeLink { get; set; }
    }
}
