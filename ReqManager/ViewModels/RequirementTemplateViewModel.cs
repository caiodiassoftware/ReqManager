using ReqManager.Entities.Acess;
using ReqManager.Entities.Requirement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReqManager.ViewModels
{
    public class RequirementTemplateViewModel
    {
        [Key]
        public int RequirementTemplateID { get; set; }
        [Display(Name = "User")]
        public int UserID { get; set; }
        public int RequirementTypeID { get; set; }
        [Required]
        [MaxLength(50), MinLength(6)]
        [Display(Name = "Template")]
        public string description { get; set; }
        [Display(Name = "Template Html")]
        [AllowHtml]
        public string templateHtml { get; set; }
        [Display(Name = "Creation Date")]
        public System.DateTime createDate { get; set; } = DateTime.Now;

        public virtual UserEntity Users { get; set; }
        public virtual RequirementTypeEntity RequirementType { get; set; }
    }
}