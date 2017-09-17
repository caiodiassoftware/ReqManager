using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReqManager.ViewModels
{
    public class ControllerActionViewModel
    {
        public int ControllerActionID { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public bool IsGet { get; set; }
    }
}