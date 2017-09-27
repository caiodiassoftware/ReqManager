using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Acess
{
    public class StatusTaskEntity
    {
        [Key]
        public int TaskStatusID { get; set; }
        [MinLength(3)]
        [MaxLength(50)]
        [Display(Name = "Description")]
        public string description { get; set; }
    }
}
