using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("REQUEST_STATUS", Schema = "REQ")]
    public partial class RequestStatus
    {
        public RequestStatus()
        {
            this.RequirementRequestForChanges = new HashSet<RequirementRequestForChanges>();
        }

        [Key]
        public int RequestStatusID { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        [Index(IsUnique = true)]
        public string description { get; set; }

        public virtual ICollection<RequirementRequestForChanges> RequirementRequestForChanges { get; set; }
    }
}
