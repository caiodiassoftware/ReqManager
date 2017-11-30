using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("HISTORY_TASK", Schema = "TASK")]
    public class HistoryTask
    {
        [Key]
        public int HistoryTaskID { get; set; }
        public int CreationUserID { get; set; }
        public int TaskID { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        [Required]
        [MaxLength(1000), MinLength(5)]
        public string description { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        public string descriptionImportance { get; set; }
        public DateTime changedDate { get; set; }

        [ForeignKey("CreationUserID")]
        public virtual Users Users { get; set; }
        public virtual Task Task { get; set; }
    }
}
