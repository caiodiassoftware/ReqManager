﻿using ReqManager.Entities.Acess;
using ReqManager.Entities.Requirement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Link
{
    public class LinkBetweenRequirementsEntity
    {
        [Key]
        public int LinkRequirementsID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int TypeLinkID { get; set; }
        [Required]
        public DateTime creationDate { get; set; }
        [Required]
        [MaxLength(25), MinLength(5)]
        public string code { get; set; }

        public virtual UserEntity Users { get; set; }
        public virtual RequirementEntity RequirementOriginID { get; set; }
        public virtual RequirementEntity RequirementTargetID { get; set; }
        public virtual TypeLinkEntity TypeLink { get; set; }
    }
}
