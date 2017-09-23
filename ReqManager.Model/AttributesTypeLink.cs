using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("ATTRIBUTES_TYPE_LINK", Schema = "LINK")]
    public class AttributesTypeLink
    {
        [Key]
        public int AttributesTypeLinkID { get; set; }
        public int AttributeID { get; set; }
        public int TypeLinkID { get; set; }

        public virtual Attributes Attributes { get; set; }
        public virtual TypeLink TypeLink { get; set; }
    }
}
