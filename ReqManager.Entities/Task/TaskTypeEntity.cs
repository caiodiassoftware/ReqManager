using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Task
{
    public class TaskTypeEntity
    {
        [Key]
        public int TypeTaskID { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        [Display(Name = "Description")]
        public string description { get; set; }
    }
}
