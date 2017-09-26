﻿using ReqManager.Entities.Acess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Project
{
    public class StakeholdersEntity
    {
        [Key]
        public int StakeholderID { get; set; }
        [Required]
        [Display(Name = "User")]
        public int UserID { get; set; }
        [Required]
        [Display(Name = "Classification")]
        public int ClassificationID { get; set; }

        public virtual UserEntity Users { get; set; }
        public virtual StakeholderClassificationEntity StakeHolderClassification { get; set; }
    }
}
