using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Link
{
    public class AttributesEntity
    {
        [Key]
        [Display(Name = "Attribute")]
        public int AttributeID { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        [Display(Name = "Attribute Description")]
        public string description { get; set; }
    }
}
