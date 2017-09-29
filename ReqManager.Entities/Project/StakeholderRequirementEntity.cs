using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Project
{
    public class StakeholderRequirementEntity
    {
        [Key]
        public int StakeHolderRequirementID { get; set; }
        [Required]
        [Display(Name = "Project Requirements")]
        public int ProjectRequirementID { get; set; }
        [Required]
        [Display(Name = "Stakeholders")]
        public int StakeHolderID { get; set; }
        [Required]
        [Display(Name = "Creation Date")]
        public DateTime creationDate { get; set; }

        public virtual ProjectRequirementsEntity ProjectRequirements { get; set; }
        public virtual StakeholdersEntity StakeHolders { get; set; }

        public String DisplayName
        {
            get
            {
                return this.StakeHolders.Users.nickName + " - " + this.ProjectRequirements.Project.description;
            }
        }

    }
}
