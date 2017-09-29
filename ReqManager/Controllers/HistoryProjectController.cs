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
using ReqManager.Services.Acess.Interfaces;

namespace ReqManager.Controllers
{
    public class HistoryProjectController : BaseController<HistoryProjectEntity>
    {
        private IHistoryProjectService historyProjectService { get; set; }
        private IProjectService projectService { get; set; }
        private IUserService userService { get; set; }

        public HistoryProjectController(
            IHistoryProjectService historyProjectService,
            IProjectService projectService,
            IUserService userService) : base(historyProjectService)
        {
            this.historyProjectService = historyProjectService;
            this.projectService = projectService;
            this.userService = userService;
        }        
    }
}
