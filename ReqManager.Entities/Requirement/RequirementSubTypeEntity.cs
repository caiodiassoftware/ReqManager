using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Requirement
{
    public class RequirementSubTypeEntity
    {
        [Key]
        public int RequirementSubTypeID { get; set; }
        public int RequirementTypeID { get; set; }
        [MinLength(5)]
        [MaxLength(50)]
        public string description { get; set; }

        public virtual RequirementTypeEntity RequirementType { get; set; }
    }
}
