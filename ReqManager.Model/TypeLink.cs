using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("TYPE_LINK", Schema = "LINK")]
    public class TypeLink
    {
        public TypeLink()
        {
            this.AttributesTypeLink = new HashSet<AttributesTypeLink>();
            this.LinkRequirements = new HashSet<LinkBetweenRequirement>();
            this.LinkRequirementsArtifacts = new HashSet<LinkBetweenRequirementsArtifacts>();
        }
    
        [Key]
        public int TypeLinkID { get; set; }
        public int UserID { get; set; }
        [Required]
        [Index(IsUnique = true)]
        [MaxLength(50), MinLength(3)]
        public string description { get; set; }
        [Required]
        public DateTime creationDate { get; set; }

        public virtual Users Users { get; set; }
        public virtual ICollection<AttributesTypeLink> AttributesTypeLink { get; set; }
        public virtual ICollection<LinkBetweenRequirement> LinkRequirements { get; set; }
        public virtual ICollection<LinkBetweenRequirementsArtifacts> LinkRequirementsArtifacts { get; set; }
    }
}
