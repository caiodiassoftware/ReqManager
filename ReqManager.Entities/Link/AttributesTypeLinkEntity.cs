using System;
using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Link
{
    public class AttributesTypeLinkEntity
    {
        [Key]
        [Display(Name = "Type Link Attributes")]
        public int AttributesTypeLinkID { get; set; }
        [Required]
        [Display(Name = "Attribute")]
        public int AttributeID { get; set; }
        [Required]
        [Display(Name = "Link Type")]
        public int TypeLinkID { get; set; }

        public String DisplayName
        {
            get
            {
                return this.Attributes.description + " - " + this.TypeLink.description;
            }
        }

        public virtual AttributesEntity Attributes { get; set; }
        public virtual TypeLinkEntity TypeLink { get; set; }
    }
}
