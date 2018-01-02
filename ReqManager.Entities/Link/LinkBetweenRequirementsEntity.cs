using ReqManager.Entities.Acess;
using ReqManager.Entities.Requirement;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Entities.Link
{
    public class LinkBetweenRequirementsEntity
    {
        [Key]
        [Display(Name = "Requirements Link")]
        public int LinkRequirementsID { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        [Display(Name = "User")]
        public int CreationUserID { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        [Range(1, Double.PositiveInfinity)]
        [Display(Name = "Type Link")]
        public int TypeLinkID { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        [Range(1, Double.PositiveInfinity)]
        [Display(Name = "Req. Origin")]
        [DisplayName("Req. Origin")]
        public int RequirementOriginID { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        [Range(1, Double.PositiveInfinity)]
        [Display(Name = "Req. Target")]
        public int RequirementTargetID { get; set; }
        [Display(Name = "Creation Data")]
        public System.DateTime creationDate { get; set; }
        [Display(Name = "R-R Code")]
        public string code { get; set; }

        public String DisplayName
        {
            get
            {
                return this.code + " - " + this.RequirementOrigin.code + " to " + this.RequirementTarget.code + " using " + this.TypeLink.description;
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
