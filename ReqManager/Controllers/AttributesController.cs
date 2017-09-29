using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReqManager.Data.DataAcess;
using ReqManager.Entities.Link;
using ReqManager.ManagerController;
using ReqManager.Services.Link.Interfaces;

namespace ReqManager.Controllers
{
    public class AttributesController : BaseController<AttributesEntity>
    {
        public AttributesController(IAttributesService service) : base(service)
        {

        }
    }
}
