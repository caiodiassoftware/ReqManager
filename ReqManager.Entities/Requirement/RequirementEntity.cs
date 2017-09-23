using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Requirement
{
    public class RequirementEntity
    {
        [Key]
        public int RequirementID { get; set; }
        public Nullable<int> RequirementTemplateID { get; set; }
        public int StakeholderID { get; set; }
        public int UserID { get; set; }
        public int RequirementStatusID { get; set; }
        public int RequirementTypeID { get; set; }
        public int StakeholderProjectID { get; set; }
        public int MeasureImportanceID { get; set; }
        [Required]
        [MaxLength(25)]
        public string code { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        [MaxLength(100), MinLength(10)]
        public string title { get; set; }
        [Required]
        public DateTime creationDate { get; set; }
        [Required]
        [MaxLength(1000), MinLength(5)]
        public string input { get; set; }
        [Required]
        [MaxLength(1000), MinLength(5)]
        public string output { get; set; }
    }
}
