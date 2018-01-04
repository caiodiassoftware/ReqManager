using System.Web.Mvc;
using ReqManager.Entities.Link;
using ReqManager.ManagerController;
using ReqManager.Services.Estructure;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;

namespace ReqManager.Controllers
{
    public class LinkRequirementAttributesController : BaseController<LinkRequirementAttributesEntity>
    {
        private IService<LinkBetweenRequirementsEntity> linkRequirementsService { get; set; }
        private IService<LinkRequirementAttributesEntity> service { get; set; }

        public LinkRequirementAttributesController(
            IService<LinkRequirementAttributesEntity> service,
            IService<AttributesEntity> attributeService,
            IService<LinkBetweenRequirementsEntity> linkRequirementsService) : base(service)
        {
            this.service = service;
            this.linkRequirementsService = linkRequirementsService;
        }

        public ActionResult CreateNewAttributeForLink(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                LinkBetweenRequirementsEntity link = linkRequirementsService.get(id);

                if (link == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                ViewData.Add("LinkRequirementsID", new SelectList(
                        new List<LinkBetweenRequirementsEntity>() { link },
                        "LinkRequirementsID", "DisplayName", id));
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult GetAttributes(int LinkRequirementsID)
        {
            try
            {
                return Json(service.getAll().Where(l => l.LinkRequirementsID.Equals(LinkRequirementsID)), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
