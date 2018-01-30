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
        [Display(Name = "Project")]
        public int ProjectID { get; set; }
        [Required]
        [Display(Name = "User")]
        public int CreationUserID { get; set; }
        [Required]
        [Display(Name = "Status")]
        public int RequirementStatusID { get; set; }
        [Required]
        [Display(Name = "Type")]
        public int RequirementTypeID { get; set; }
        [Required]
        [Display(Name = "SubType")]
        public Nullable<int> RequirementSubTypeID { get; set; }
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
        [DataType(DataType.MultilineText)]
        public string title { get; set; }
        [Required]
        [Display(Name = "Creation Date")]
        public System.DateTime creationDate { get; set; }
        [Required]
        public bool preTraceability { get; set; }
        public Nullable<DateTime> startDate { get; set; }
        public Nullable<DateTime> endDate { get; set; }
        [Required]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(0.00, 9999999999999999.99)]
        public decimal cost { get; set; }
        [Required]
        public bool active { get; set; }
        [MaxLength(1000), MinLength(10)]
        public string rationale { get; set; }

        public String DisplayName
        {
            get
            {
                return this.RequirementType.abbreviation + " - " + this.code + " : " + this.RequirementStatus.description;
            }
        }

        public String DisplayType
        {
            get
            {
                return this.RequirementType.description;
            }
        }

        public virtual UserEntity Users { get; set; }
        public virtual ProjectEntity Project { get; set; }
        public virtual ImportanceEntity Importance { get; set; }
        public virtual RequirementTemplateEntity RequirementTemplate { get; set; }
        public virtual RequirementStatusEntity RequirementStatus { get; set; }
        public virtual RequirementTypeEntity RequirementType { get; set; }
        public virtual RequirementSubTypeEntity RequirementSubType { get; set; }
    }
}
