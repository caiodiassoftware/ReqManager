using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("REQUIREMENT_SUB_TYPE", Schema = "REQ")]
    public class RequirementSubType
    {
        [Key]
        public int RequirementSubTypeID { get; set; }
        public int RequirementTypeID { get; set; }
        [MinLength(5)]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string description { get; set; }

        public virtual RequirementType RequirementType { get; set; }
    }
}
