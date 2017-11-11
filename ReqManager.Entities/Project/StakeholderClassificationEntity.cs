using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Acess
{
    public class StakeholderClassificationEntity
    {
        [Key]
        [Display(Name = "Classification")]
        public int ClassificationID { get; set; }
        [MinLength(6)]
        [MaxLength(50)]
        [Display(Name = "Classification Description")]
        public string description { get; set; }

    }
}
