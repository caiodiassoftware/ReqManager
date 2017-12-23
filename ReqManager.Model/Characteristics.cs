using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("CHARACTERISTICS", Schema = "REQ")]
    public class Characteristics
    {
        public Characteristics()
        {
            this.RequirementCharacteristics = new HashSet<RequirementCharacteristics>();
        }

        [Key]
        public int CharacteristicsID { get; set; }
        [MaxLength(30)]
        [Required]
        [Index(IsUnique = true)]
        public string name { get; set; }
        [Required]
        [MaxLength(500)]
        public string description { get; set; }
        [Required]
        public bool required { get; set; }

        public virtual ICollection<RequirementCharacteristics> RequirementCharacteristics { get; set; }
    }
}
