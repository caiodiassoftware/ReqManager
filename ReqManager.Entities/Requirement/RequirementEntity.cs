﻿using ReqManager.Entities.Acess;
using ReqManager.Entities.Project;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [Display(Name = "User")]
        public int UserID { get; set; }
        [Required]
        [Display(Name = "Status")]
        public int RequirementStatusID { get; set; }
        [Required]
        [Display(Name = "Type")]
        public int RequirementTypeID { get; set; }
        [Required]
        [Display(Name = "StakeholderProject")]
        public int StakeholdersProjectID { get; set; }
        [Required]
        [Display(Name = "Importance")]
        public int MeasureImportanceID { get; set; }
        [ReadOnly(true)]
        [Display(Name = "Req. Code")]
        public string code { get; set; }
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
                return code + " - " + MeasureImportance.description;
            }
        }

        public virtual UserEntity Users { get; set; }
        public virtual StakeholdersProjectEntity StakeholderProject { get; set; }
        public virtual MeasureImportanceEntity MeasureImportance { get; set; }
        public virtual RequirementTemplateEntity RequirementTemplate { get; set; }
        public virtual RequirementStatusEntity RequirementStatus { get; set; }
        public virtual RequirementTypeEntity RequirementType { get; set; }
    }
}
