using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("LINK_BETWEEN_REQUIREMENTS_ARTIFACTS", Schema = "LINK")]
    public class LinkBetweenRequirementsArtifacts
    {
        public LinkBetweenRequirementsArtifacts()
        {
            this.LinkArtifactAttributes = new HashSet<LinkArtifactAttributes>();
        }
    
        [Key]
        public int LinkArtifactRequirementID { get; set; }
        public int CreationUserID { get; set; }
        [Index("IX_artifact_requirement", 1, IsUnique = true)]
        public int ProjectArtifactID { get; set; }
        [Index("IX_artifact_requirement", 2, IsUnique = true)]
        public int RequirementID { get; set; }
        [Index("IX_artifact_requirement", 3, IsUnique = true)]
        public int TypeLinkID { get; set; }
        [Required]
        public DateTime creationDate { get; set; }
        [MaxLength(25)]
        [Index(IsUnique = true)]
        public string code { get; set; }

        [ForeignKey("CreationUserID")]
        public virtual Users Users { get; set; }
        public virtual ProjectArtifact ProjectArtifact { get; set; }
        public virtual Requirement Requirement { get; set; }
        public virtual ICollection<LinkArtifactAttributes> LinkArtifactAttributes { get; set; }        
        public virtual TypeLink TypeLink { get; set; }
    }
}
