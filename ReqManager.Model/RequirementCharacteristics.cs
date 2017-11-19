using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("REQUIREMENT_CHARACTERISTICS", Schema = "REQ")]
    public class RequirementCharacteristics
    {
        [Key]
        public int RequirementCharacteristicsID { get; set; }
        public int CharacteristicsID { get; set; }
        public int RequirementID { get; set; }
        public bool active { get; set; }

        public virtual Characteristics Characteristics { get; set; }
        public virtual Requirement Requirement { get; set; }
    }
}
