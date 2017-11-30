using ReqManager.Entities.Acess;
using ReqManager.Entities.Project;
using System;
using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Artifact
{
    public class ProjectArtifactEntity
    {
        [Key]
        [Display(Name = "Artifact")]
        public int ProjectArtifactID { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        [Display(Name = "User")]
        public int CreationUserID { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        [Range(1, Double.PositiveInfinity)]
        [Display(Name = "Artifact Type")]
        public int ArtifactTypeID { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        [Range(1, Double.PositiveInfinity)]
        [Display(Name = "Measure Importance")]
        public int ImportanceID { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        [Range(1, Double.PositiveInfinity)]
        [Display(Name = "Project")]
        public int ProjectID { get; set; }
        [Display(Name = "Art. Code")]
        public string code { get; set; }
        [Required]
        [MaxLength(500)]
        [Display(Name = "Path")]
        public string path { get; set; }
        [Required]
        [MaxLength(500)]
        [Display(Name = "Artifact Description")]
        public string description { get; set; }
        [Display(Name = "Creation Data")]
        public DateTime creationDate { get; set; } = DateTime.Now;

        public String DisplayName
        {
            get
            {
                return this.description + " - " + this.code + " : " + this.ArtifactType.description;
            }
        }

        public virtual UserEntity Users { get; set; }
        public virtual ArtifactTypeEntity ArtifactType { get; set; }
        public virtual ImportanceEntity Importance { get; set; }
        public virtual ProjectEntity Project { get; set; }
    }
}
