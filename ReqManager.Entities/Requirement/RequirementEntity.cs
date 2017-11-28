using ReqManager.Entities.Acess;
using ReqManager.Entities.Project;
using System;
using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Requirement
{
    public class RequirementEntity
    {
        [Key]
        [Display(Name = "Requirement")]
        public int RequirementID { get; set; }
        [Display(Name = "Template")]
        public Nullable<int> RequirementTemplateID { get; set; }
        [Required]
        [Display(Name = "User")]
        public int UserID { get; set; }
        [Required]
        [Display(Name = "Status")]
        public int RequirementStatusID { get; set; }
        [Required]
        [Display(Name = "Type")]
        public int RequirementTypeID { get; set; }
        [Required]
        [Display(Name = "SubType")]
        public int RequirementSubTypeID { get; set; }
        [Required]
        [Display(Name = "Stakeholder Project")]
        public int StakeholdersProjectID { get; set; }
        [Required]
        [Display(Name = "Importance")]
        public int ImportanceID { get; set; }
        [Display(Name = "Req. Code")]
        public string code { get; set; }
        public int versionNumber { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string description { get; set; }
        [Required]
        [MaxLength(100), MinLength(10)]
        [Display(Name = "Title")]
        public string title { get; set; }
        [Required]
        [Display(Name = "Creation Date")]
        public System.DateTime creationDate { get; set; } = DateTime.Now;

        public String DisplayName
        {
            get
            {
                return code + " - " + Importance.description;
            }
        }

        public virtual UserEntity Users { get; set; }
        public virtual StakeholdersProjectEntity StakeholderProject { get; set; }
        public virtual ImportanceEntity Importance { get; set; }
        public virtual RequirementTemplateEntity RequirementTemplate { get; set; }
        public virtual RequirementStatusEntity RequirementStatus { get; set; }
        public virtual RequirementTypeEntity RequirementType { get; set; }
        public virtual RequirementSubTypeEntity RequirementSubType { get; set; }
    }
}
