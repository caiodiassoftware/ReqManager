using ReqManager.Entities.Acess;
using System;
using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Project
{
    public class StakeholdersEntity
    {
        [Key]
        [Display(Name = "Stakeholder")]
        public int StakeholderID { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        [Display(Name = "User")]
        public int UserID { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        [Range(1, Double.PositiveInfinity)]
        [Display(Name = "Classification")]
        public int ClassificationID { get; set; }

        public String DisplayName
        {
            get
            {
                return this.Users.nickName + " - " + this.StakeHolderClassification.description;
            }
        }

        public virtual UserEntity Users { get; set; }
        public virtual StakeholderClassificationEntity StakeHolderClassification { get; set; }
    }
}
