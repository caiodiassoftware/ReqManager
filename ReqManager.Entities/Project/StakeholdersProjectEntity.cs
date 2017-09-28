using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Project
{
    public class StakeholdersProjectEntity
    {
        [Key]
        public int StakeholdersProjectID { get; set; }
        [Required]
        [Display(Name = "Project")]
        public int ProjectID { get; set; }
        [Required]
        [Display(Name = "Stakeholder")]
        public int StakeholderID { get; set; }
        [Required]
        [Display(Name = "Creation Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Invalid Date")]
        public DateTime creationDate { get; set; }

        public String DisplayName
        {
            get
            {
                return this.Project.description + " - " + this.Stakeholders.DisplayName;
            }
        }

        public virtual ProjectEntity Project { get; set; }
        public virtual StakeholdersEntity Stakeholders { get; set; }
    }
}
