using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using ReqManager.Entities.Link;
using ReqManager.ManagerController;
using ReqManager.Services.Link.Interfaces;
using System.Net;
using System.Collections.Generic;

namespace ReqManager.Controllers
{
    public class AttributesTypeLinkController : BaseController<AttributesTypeLinkEntity>
    {
        private ITypeLinkService typeLinkService { get; set; }
        private IAttributesTypeLinkService service { get; set; }

        public AttributesTypeLinkController(
            IAttributesTypeLinkService service,
            IAttributesService attributeService,
            ITypeLinkService typeLinkService) : base(service)
        {
            this.service = service;
            this.typeLinkService = typeLinkService;

            ViewData.Add("AttributeID", new SelectList(attributeService.getAll(), "AttributeID", "description"));
        }

        #region GETS

        public ActionResult CreateNewAttributeForTypeLink(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                TypeLinkEntity type = typeLinkService.get(id);

                if (type == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                ViewData.Add("TypeLinkID", new SelectList(
                        new List<TypeLinkEntity>() { type }, "TypeLinkID", "DisplayName", id));
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult GetAttributesOfTypeLink(int type)
        {
            try
            {
                return Json(service.GetByTypeLink(type).Select(a => a.Attributes), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
