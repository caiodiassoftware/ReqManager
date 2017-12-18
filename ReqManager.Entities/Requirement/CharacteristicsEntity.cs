using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Project
{
    public class CharacteristicsEntity
    {
        [Key]
        public int CharacteristicsID { get; set; }
        [MaxLength(30)]
        [Required]
        public string name { get; set; }
        [Required]
        [MaxLength(255)]
        [DataType(DataType.MultilineText)]
        public string description { get; set; }
        [Required]
        public bool active { get; set; }
    }
}
