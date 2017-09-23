using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReqManager.Data.DataAcess;
using ReqManager.Entities.Project;
using ReqManager.ManagerController;
using ReqManager.Services.Estructure;
using ReqManager.Services.Project.Interfaces;

namespace ReqManager.Controllers
{
    public class ProjectPhasesController : BaseController<ProjectPhasesEntity>
    {
        public ProjectPhasesController(IProjectPhasesService service) : base(service)
        {
        }
    }
}
