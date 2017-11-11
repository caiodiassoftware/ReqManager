using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Artifact
{
    public class ArtifactTypeEntity
    {
        [Key]
        [Display(Name = "Artifact Type")]
        public int ArtifactTypeID { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        [Display(Name = "Artifact Type Description")]
        public string description { get; set; }
    }
}
