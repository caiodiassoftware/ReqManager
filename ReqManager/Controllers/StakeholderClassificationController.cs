using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReqManager.Data.DataAcess;
using ReqManager.Entities.Acess;
using ReqManager.ManagerController;
using ReqManager.Services.Estructure;
using ReqManager.Services.Project.Interfaces;

namespace ReqManager.Views
{
    public class StakeholderClassificationController : BaseController<StakeholderClassificationEntity>
    {
        public StakeholderClassificationController(IStakeholderClassificationService service) : base(service)
        {
        }
    }
}
