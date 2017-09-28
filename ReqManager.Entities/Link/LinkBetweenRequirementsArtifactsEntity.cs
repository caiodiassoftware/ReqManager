using ReqManager.Entities.Acess;
using ReqManager.Entities.Artifact;
using ReqManager.Entities.Requirement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Link
{
    public class LinkBetweenRequirementsArtifactsEntity
    {
        [Key]
        public int LinkArtifactRequirementID { get; set; }
        [Required]
        [Display(Name = "User")]
        public int UserID { get; set; }
        [Required]
        [Display(Name = "Artifact")]
        public int ProjectArtifactID { get; set; }
        [Required]
        [Display(Name = "Requirement")]
        public int RequirementID { get; set; }
        [Required]
        [Display(Name = "Type Link")]
        public int TypeLinkID { get; set; }
        [Required]
        [Display(Name = "Creation Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Invalid Date")]
        public DateTime creationDate { get; set; }
        [Required]
        [MaxLength(25)]
        [Display(Name = "Code")]
        public string code { get; set; }

        public String DisplayName
        {
            get
            {
                return this.Requirement.code + " - " + this.ProjectArtifact.code + " - " + this.TypeLink.description;
            }
        }

        public virtual UserEntity Users { get; set; }
        public virtual ProjectArtifactEntity ProjectArtifact { get; set; }
        public virtual RequirementEntity Requirement { get; set; }
        public virtual TypeLinkEntity TypeLink { get; set; }
    }
}
