using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReqManager.ViewModels
{
    public class UserRoleViewModel
    {
        [Key]
        public int UserRoleID { get; set; }
        [Required]
        public string roleDescription { get; set; }
        [Required]
        public string userName { get; set; }
    }
}