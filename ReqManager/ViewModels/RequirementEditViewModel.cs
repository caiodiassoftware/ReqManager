using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ReqManager.ViewModels
{
    public class RequirementEditViewModel
    {
        [Key]
        public int RequirementID { get; set; }
        public int RequirementRequestForChangesID { get; set; }
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
        [Display(Name = "SubType")]
        public int RequirementSubTypeID { get; set; }
        [Required]
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
        [MaxLength(1000), MinLength(10)]
        [Display(Name = "Rationale")]
        public string rationale { get; set; }
        [Required]
        [Display(Name = "Creation Date")]
        public System.DateTime creationDate { get; set; } = DateTime.Now;
        [Required]
        public bool preTraceability { get; set; }
    }
}