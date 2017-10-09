using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReqManager.ViewModels
{
    public class TrackingViewModel
    {
        public TrackingViewModel()
        {
            files = new List<String>();
        }

        [Key]
        public string item { get; set; }
        public string path { get; set; }
        public List<String> files { get; set; }        
    }
}