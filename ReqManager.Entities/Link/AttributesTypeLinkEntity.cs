using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Link
{
    public class AttributesTypeLinkEntity
    {
        [Key]
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
