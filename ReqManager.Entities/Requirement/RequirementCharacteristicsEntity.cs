using ReqManager.Entities.Project;
using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Requirement
{
    public class RequirementCharacteristicsEntity
    {
        [Key]
        public int RequirementCharacteristicsID { get; set; }
        public int CharacteristicsID { get; set; }
        public int RequirementID { get; set; }
        public bool active { get; set; }

        public virtual CharacteristicsEntity Characteristics { get; set; }
        public virtual RequirementEntity Requirement { get; set; }
    }
}
