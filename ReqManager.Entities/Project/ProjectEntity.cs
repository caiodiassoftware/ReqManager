using ReqManager.Entities.Acess;
using System;
using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Project
{
    public class ProjectEntity
    {
        [Key]
        [Display(Name = "Project")]
        public int ProjectID { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        [Display(Name = "User")]
        public int CreationUserID { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        [Range(1, Double.PositiveInfinity)]
        [Display(Name = "Phases")]
        public int ProjectPhasesID { get; set; }
        [Required]
        [MaxLength(255)]
        [Display(Name = "Project Description")]
        [DataType(DataType.MultilineText)]
        public string description { get; set; }
        [Required]
        [MaxLength(300)]
        [Display(Name = "Path")]
        public string pathForTraceability { get; set; }
        [Required]
        [MaxLength(1000)]
        [Display(Name = "Environmental Information")]
        [DataType(DataType.MultilineText)]
        public string environmentalInformation { get; set; }
        [Required]
        [MaxLength(1000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Management Information")]
        public string managementInformation { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime startDate { get; set; }
        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime endDate { get; set; }
        [Display(Name = "Creation Date")]
        public DateTime creationDate { get; set; } = DateTime.Now;
        [Display(Name = "Prj. Code")]
        public string code { get; set; }

        public String DisplayName
        {
            get
            {
                return this.code;
            }
        }

        public virtual UserEntity Users { get; set; }
        public virtual ProjectPhasesEntity ProjectPhases { get; set; }
    }
}
