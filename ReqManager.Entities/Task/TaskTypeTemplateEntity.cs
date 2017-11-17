using System;
using System.ComponentModel.DataAnnotations;
namespace ReqManager.Entities.Acess
{
    public class TaskTypeTemplateEntity
    {
        [Key]
        public int TaskTypeTemplateID { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        [Display(Name = "User")]
        public int UserID { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        [Range(1, Double.PositiveInfinity)]
        [Display(Name = "Type")]
        public int TaskTypeID { get; set; }
        [Required]
        [MaxLength(1000)]
        [Display(Name = "Template Html")]
        public string templateHtml { get; set; }
        [Display(Name = "Creation Date")]
        public DateTime creationDate { get; set; } = DateTime.Now;

        public virtual UserEntity Users { get; set; }
        public virtual TaskTypeEntity TaskType { get; set; }
    }
}
