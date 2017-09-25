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
        public int UserID { get; set; }
        [Required]
        public int ProjectArtifactID { get; set; }
        [Required]
        public int RequirementID { get; set; }
        [Required]
        public int TypeLinkID { get; set; }
        [Required]
        public DateTime creationDate { get; set; }
        [Required]
        [MaxLength(25)]
        public string code { get; set; }

        public virtual UserEntity Users { get; set; }
        public virtual ProjectArtifactEntity ProjectArtifact { get; set; }
        public virtual RequirementEntity Requirement { get; set; }
        public virtual TypeLinkEntity TypeLink { get; set; }
    }
}
