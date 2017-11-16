using System;
using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Acess
{
    public class UserTaskEntity
    {
        [Key]
        public int UserTaskID { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        [Range(1, Double.PositiveInfinity)]
        [Display(Name = "User")]
        public int UserID { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        [Range(1, Double.PositiveInfinity)]
        [Display(Name = "Task")]
        public int TaskID { get; set; }
        [Display(Name = "Creation Date")]
        public DateTime creationDate { get; set; } = DateTime.Now;

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
