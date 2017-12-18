using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Requirement
{
    public partial class RequestStatusEntity
    {
        [Key]
        public int RequestStatusID { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        [DataType(DataType.MultilineText)]
        public string description { get; set; }
    }
}
