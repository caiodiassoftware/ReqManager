using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("HISTORY_PROJECT", Schema = "PROJ")]
    public class HistoryProject
    {
        [Key]
        public int HistoryProjectID { get; set; }
        public int CreationUserID { get; set; }
        public int ProjectID { get; set; }
        [Required]
        [MaxLength(50)]
        public string descriptionPhases { get; set; }
        [Required]
        public DateTime startDate { get; set; }
        [Required]
        public DateTime endDate { get; set; }
        [Required]
        public DateTime changedDate { get; set; }

        [ForeignKey("CreationUserID")]
        public virtual Users Users { get; set; }
        public virtual Project Project { get; set; }
    }
}
