using ReqManager.Entities.Acess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Acess
{
    public class UserTaskEntity
    {
        [Key]
        public int UserTaskID { get; set; }
        [Required]
        [Display(Name = "User")]
        public int UserID { get; set; }
        [Required]
        [Display(Name = "Task")]
        public int TaskID { get; set; }
        [Required]
        [Display(Name = "Creation Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Invalid Date")]
        public DateTime creationDate { get; set; }

        public String DisplayName
        {
            get
            {
                return this.User.nickName + " - " + this.Task.DisplayName;
            }
        }

        public virtual UserEntity User { get; set; }
        public virtual TaskEntity Task { get; set; }
    }
}
