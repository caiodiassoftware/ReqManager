using System.ComponentModel.DataAnnotations;

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
        [DataType(DataType.MultilineText)]
        public string description { get; set; }
    }
}
