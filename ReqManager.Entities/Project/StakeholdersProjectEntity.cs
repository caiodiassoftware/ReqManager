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
        public int ProjectID { get; set; }
        [Required]
        public int StakeholderID { get; set; }
        [Required]
        public DateTime creationDate { get; set; }

        public virtual ProjectEntity Project { get; set; }
        public virtual StakeholdersEntity Stakeholders { get; set; }
    }
}
