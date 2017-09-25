using ReqManager.Entities.Acess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Task
{
    public class HistoryTaskEntity
    {
        [Key]
        public int HistoryTaskID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int TaskID { get; set; }
        [Required]
        public DateTime startDate { get; set; }
        [Required]
        public DateTime endDate { get; set; }
        [Required]
        [MaxLength(1000), MinLength(5)]
        public string description { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        public string descriptionMeasureImportance { get; set; }
        public DateTime changedDate { get; set; }

        public virtual UserEntity Users { get; set; }
        public virtual TaskEntity Task { get; set; }
    }
}
