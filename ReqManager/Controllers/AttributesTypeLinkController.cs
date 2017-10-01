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
    public class AttributesTypeLinkController : BaseController<AttributesTypeLinkEntity>
    {
        public AttributesTypeLinkController(
            IAttributesTypeLinkService service,
            IAttributesService attributeService,
            ITypeLinkService typeLinkService) : base(service)
        {
            ViewData.Add("AttributeID", new SelectList(attributeService.getAll(), "AttributeID", "description"));
            ViewData.Add("TypeLinkID", new SelectList(typeLinkService.getAll(), "TypeLinkID", "description"));
        }
    }
}
