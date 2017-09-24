using ReqManager.Entities.Acess;
using ReqManager.Entities.Project;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Artifact
{
    public class ProjectArtifactEntity
    {
        [Key]
        public int ProjectArtifactID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int ArtifactTypeID { get; set; }
        [Required]
        public int MeasureImportanceID { get; set; }
        [Required]
        public int ProjectID { get; set; }
        [Required]
        [MaxLength(25), MinLength(5)]
        public string code { get; set; }
        [Required]
        [MaxLength(500)]
        public string path { get; set; }
        [Required]
        [MaxLength(500)]
        public string description { get; set; }
        [Required]
        public DateTime creationDate { get; set; }

        public virtual UserEntity Users { get; set; }
        public virtual ArtifactTypeEntity ArtifactType { get; set; }
        public virtual MeasureImportanceEntity MeasureImportance { get; set; }
        public virtual ProjectEntity Project { get; set; }
    }
}
