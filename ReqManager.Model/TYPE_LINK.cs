using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("TYPE_LINK", Schema = "LINK")]
    public class TYPE_LINK
    {
        public TYPE_LINK()
        {
            this.AttributesTypeLink = new HashSet<ATTRIBUTES_TYPE_LINK>();
            this.LinkRequirements = new HashSet<LINK_BETWEEN_REQUIREMENT>();
            this.LinkRequirementsArtifacts = new HashSet<LINK_BETWEEN_REQUIREMENTS_ARTIFACTS>();
        }
    
        [Key]
        public int TypeLinkID { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public DateTime creationDate { get; set; }

        public virtual USERS Users { get; set; }
        public virtual ICollection<ATTRIBUTES_TYPE_LINK> AttributesTypeLink { get; set; }
        public virtual ICollection<LINK_BETWEEN_REQUIREMENT> LinkRequirements { get; set; }
        public virtual ICollection<LINK_BETWEEN_REQUIREMENTS_ARTIFACTS> LinkRequirementsArtifacts { get; set; }
    }
}
