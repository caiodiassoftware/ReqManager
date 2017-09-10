using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("HISTORY_TASK", Schema = "TASK")]
    public class HISTORY_TASK
    {
        [Key]
        public int HistoryTaskID { get; set; }
        public System.DateTime startDate { get; set; }
        public System.DateTime endDate { get; set; }
        [Required]
        [MaxLength(1000), MinLength(5)]
        public string description { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        public string descriptionMeasureImportance { get; set; }
        public System.DateTime changedDate { get; set; }
    
        public virtual USERS Users { get; set; }
        public virtual TASK Task { get; set; }
    }
}
