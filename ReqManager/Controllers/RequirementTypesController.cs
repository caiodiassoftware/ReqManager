using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReqManager.Data.DataAcess;
using ReqManager.Entities.Requirement;
using ReqManager.ManagerController;
using ReqManager.Services.Estructure;
using ReqManager.Services.Requirements.Interfaces;

namespace ReqManager.Controllers
{
    public class RequirementTypesController : BaseController<RequirementTypeEntity>
    {
        public RequirementTypesController(IRequirementTypeService service) : base(service)
        {
        }
    }
}
