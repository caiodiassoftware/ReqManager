using ReqManager.Entities.Acess;
using ReqManager.Entities.Project;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Artifact
{
    public class ProjectArtifactEntity
    {
        [Key]
        [Display(Name = "Artifact")]
        public int ProjectArtifactID { get; set; }
        [Required]
        [Display(Name = "User")]
        public int UserID { get; set; }
        [Required]
        [Display(Name = "Artifact Type")]
        public int ArtifactTypeID { get; set; }
        [Required]
        [Display(Name = "Measure Importance")]
        public int MeasureImportanceID { get; set; }
        [Required]
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
                return this.description + " - " + this.code;
            }
        }

        public virtual UserEntity Users { get; set; }
        public virtual ArtifactTypeEntity ArtifactType { get; set; }
        public virtual MeasureImportanceEntity MeasureImportance { get; set; }
        public virtual ProjectEntity Project { get; set; }
    }
}
