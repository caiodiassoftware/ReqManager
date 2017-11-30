using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("LINK_BETWEEN_REQUIREMENT", Schema = "LINK")]
    public class LinkBetweenRequirement
    {
        public LinkBetweenRequirement()
        {
            this.linkRequirementAttributes = new HashSet<LinkRequirementAttributes>();
        }

        [Key]
        public int LinkRequirementsID { get; set; }
        public int CreationUserID { get; set; }
        public int TypeLinkID { get; set; }
        [Index("IX_LINK_BETWEEN_REQUIREMENT", 1, IsUnique = true)]
        public int RequirementOriginID { get; set; }
        [Index("IX_LINK_BETWEEN_REQUIREMENT", 2, IsUnique = true)]
        public int RequirementTargetID { get; set; }
        [Required]  
        public DateTime creationDate { get; set; }
        [MaxLength(25), MinLength(4)]
        [Index(IsUnique = true)]
        public string code { get; set; }

        [ForeignKey("CreationUserID")]
        public virtual Users Users { get; set; }
        public virtual ICollection<LinkRequirementAttributes> linkRequirementAttributes { get; set; }
        [ForeignKey("RequirementOriginID")]
        [InverseProperty("LinkRequirementsOrigin")]
        public virtual Requirement RequirementOrigin { get; set; }
        [ForeignKey("RequirementTargetID")]
        [InverseProperty("LinkRequirementsTarget")]
        public virtual Requirement RequirementTarget { get; set; }
        public virtual TypeLink TypeLink { get; set; }
    }
}
