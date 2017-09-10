using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("HISTORY_PROJECT", Schema = "PROJ")]
    public class HISTORY_PROJECT
    {
        [Key]
        public int HistoryProjectID { get; set; }
        [Required]
        [MaxLength(50)]
        public string descriptionPhases { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public DateTime changedDate { get; set; }
    
        public virtual USERS Users { get; set; }
        public virtual PROJECT Project { get; set; }
    }
}
