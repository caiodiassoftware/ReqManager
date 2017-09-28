using ReqManager.Entities.Acess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Acess
{
    public class HistoryTaskEntity
    {
        [Key]
        public int HistoryTaskID { get; set; }
        [Required]
        [Display(Name = "User")]
        public int UserID { get; set; }
        [Required]
        [Display(Name = "Task")]
        public int TaskID { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Invalid Date")]
        public DateTime startDate { get; set; }
        [Required]
        [Display(Name = "End Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Invalid Date")]
        public DateTime endDate { get; set; }
        [Required]
        [MaxLength(1000), MinLength(5)]
        [Display(Name = "Description")]
        public string description { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        [Display(Name = "Importance")]
        public string descriptionMeasureImportance { get; set; }
        [Display(Name = "Changed Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Invalid Date")]
        public DateTime changedDate { get; set; }

        public virtual UserEntity Users { get; set; }
        public virtual TaskEntity Task { get; set; }
    }
}
