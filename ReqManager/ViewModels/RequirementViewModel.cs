using ReqManager.Entities.Acess;
using ReqManager.Entities.Link;
using ReqManager.Entities.Project;
using ReqManager.Entities.Requirement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ReqManager.ViewModels
{
    public class RequirementViewModel
    {
        public RequirementViewModel()
        {
            linkReq = new List<LinkBetweenRequirementsEntity>();
            linkReqArt = new List<LinkBetweenRequirementsArtifactsEntity>();
            characteristics = new List<RequirementCharacteristicsEntity>();
            stakeholdersApproval = new List<StakeholderRequirementApprovalEntity>();
            request = new List<RequirementRequestForChangesEntity>();
            versions = new List<RequirementVersionsEntity>();
            stakeholders = new List<StakeholderRequirementEntity>();
        }

        [Key]
        public int RequirementID { get; set; }
        [Required]
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
        [Display(Name = "Version Number")]
        public int versionNumber { get; set; }
        [Required]
        [Display(Name = "Importance")]
        public int ImportanceID { get; set; }
        [Display(Name = "Req. Code")]
        public string code { get; set; }
        [Required]
        [AllowHtml]
        [Display(Name = "Description")]
        public string description { get; set; }
        [Required]
        [MaxLength(100), MinLength(10)]
        [Display(Name = "Requirement Title")]
        public string title { get; set; }
        [Required]
        [Display(Name = "Creation Date")]
        public System.DateTime creationDate { get; set; } = DateTime.Now;
        [Required]
        public bool preTraceability { get; set; }
        [Display(Name = "Start Date")]
        public Nullable<DateTime> startDate { get; set; }
        [Display(Name = "End Date")]
        public Nullable<DateTime> endDate { get; set; }
        [Required]
        [Display(Name = "Cost")]
        [Range(0, 9999999999999999.99)]
        public decimal cost { get; set; }
        [Required]
        public bool active { get; set; }

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

        public virtual RequirementSubTypeEntity RequirementSubType { get; set; }
        public virtual UserEntity Users { get; set; }
        public virtual ProjectEntity Project { get; set; }
        public virtual ImportanceEntity Importance { get; set; }
        public virtual RequirementTemplateEntity RequirementTemplate { get; set; }
        public virtual RequirementStatusEntity RequirementStatus { get; set; }
        public virtual RequirementTypeEntity RequirementType { get; set; }

        public List<LinkBetweenRequirementsEntity> linkReq { get; set; }
        public List<LinkBetweenRequirementsArtifactsEntity> linkReqArt { get; set; }
        public List<RequirementCharacteristicsEntity> characteristics { get; set; }
        public List<StakeholderRequirementApprovalEntity> stakeholdersApproval { get; set; }
        public List<StakeholderRequirementEntity> stakeholders { get; set; }
        public List<RequirementRequestForChangesEntity> request { get; set; }
        public List<RequirementVersionsEntity> versions { get; set; }
    }
}