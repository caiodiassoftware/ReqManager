using ReqManager.Entities.Acess;
using ReqManager.Entities.Project;
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
        [Required]
        public int StakeholderID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int RequirementStatusID { get; set; }
        [Required]
        public int RequirementTypeID { get; set; }
        [Required]
        public int StakeholderProjectID { get; set; }
        [Required]
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

        public virtual UserEntity Users { get; set; }
        public virtual StakeholdersProjectEntity StakeholderProject { get; set; }
        public virtual MeasureImportanceEntity MeasureImportance { get; set; }
        public virtual RequirementTemplateEntity RequirementTemplate { get; set; }
        public virtual RequirementStatusEntity RequirementStatus { get; set; }
        public virtual RequirementTypeEntity RequirementType { get; set; }
    }
}
