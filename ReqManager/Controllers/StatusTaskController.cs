﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReqManager.Data.DataAcess;
using ReqManager.Model;
using ReqManager.Services.Task.Interfaces;
using ReqManager.ManagerController;

namespace ReqManager.Controllers
{
    public class StatusTaskController : BaseController<StatusTask>
    {
        public StatusTaskController(IStatusTaskService service) : base(service)
        {

        }
    }
}
