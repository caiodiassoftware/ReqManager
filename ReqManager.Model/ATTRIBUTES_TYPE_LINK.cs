using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("ATTRIBUTES_TYPE_LINK", Schema = "LINK")]
    public class ATTRIBUTES_TYPE_LINK
    {
        [Key]
        public int AttributesTypeLinkID { get; set; }
    
        public virtual ATTRIBUTES Attributes { get; set; }
        public virtual TYPE_LINK TypeLink { get; set; }
    }
}
