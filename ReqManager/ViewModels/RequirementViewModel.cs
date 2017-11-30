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
            stakeholders = new List<StakeholderRequirementEntity>();
            request = new List<RequirementRequestForChangesEntity>();
            versions = new List<RequirementVersionsEntity>();
            projectRequirement = new List<ProjectRequirementsEntity>();
        }

        [Key]
        public int RequirementID { get; set; }
        [Required]
        [Display(Name = "Template")]
        public int RequirementTemplateID { get; set; }
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
        public int RequirementSubTypeID { get; set; }
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

        public String DisplayName
        {
            get
            {
                return code + " - " + Importance.description;
            }
        }

        public virtual RequirementSubTypeEntity RequirementSubType { get; set; }
        public virtual UserEntity Users { get; set; }
        public virtual ProjectEntity Project { get; set; }
        public virtual StakeholdersProjectEntity StakeholderProject { get; set; }
        public virtual ImportanceEntity Importance { get; set; }
        public virtual RequirementTemplateEntity RequirementTemplate { get; set; }
        public virtual RequirementStatusEntity RequirementStatus { get; set; }
        public virtual RequirementTypeEntity RequirementType { get; set; }

        public List<LinkBetweenRequirementsEntity> linkReq { get; set; }
        public List<LinkBetweenRequirementsArtifactsEntity> linkReqArt { get; set; }
        public List<RequirementCharacteristicsEntity> characteristics { get; set; }
        public List<StakeholderRequirementEntity> stakeholders { get; set; }
        public List<RequirementRequestForChangesEntity> request { get; set; }
        public List<RequirementVersionsEntity> versions { get; set; }
        public List<ProjectRequirementsEntity> projectRequirement { get; set; }
    }
}