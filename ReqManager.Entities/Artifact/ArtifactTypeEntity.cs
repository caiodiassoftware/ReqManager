using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Artifact
{
    public class ArtifactTypeEntity
    {
        [Key]
        public int ArtefactTypeID { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        public string description { get; set; }
    }
}
