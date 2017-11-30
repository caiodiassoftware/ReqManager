using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
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

        public JsonResult GetAttributesOfTypeLink(int type)
        {
            try
            {
                var item = Service.getAll().Where(a => a.TypeLinkID.Equals(type)).Select(a => a.Attributes);
                return Json(item, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
