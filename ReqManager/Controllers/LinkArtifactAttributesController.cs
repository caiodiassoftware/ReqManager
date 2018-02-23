using System.Web.Mvc;
using ReqManager.Entities.Link;
using ReqManager.ManagerController;
using ReqManager.Services.Estructure;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;

namespace ReqManager.Controllers
{
    public class LinkArtifactAttributesController : BaseController<LinkArtifactAttributesEntity>
    {
        private IService<LinkBetweenRequirementsArtifactsEntity> reqArtService { get; set; }
        private IService<LinkArtifactAttributesEntity> service { get; set; }

        public LinkArtifactAttributesController(
            IService<LinkArtifactAttributesEntity> service,
            IService<AttributesEntity> attributeService,
            IService<LinkBetweenRequirementsArtifactsEntity> reqArtService) : base(service)
        {
            this.reqArtService = reqArtService;
            this.service = service;
        }

        public ActionResult CreateNewAttributeForLink(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new ArgumentException("Invalid Request!");
                }

                LinkBetweenRequirementsArtifactsEntity link = reqArtService.get(id);

                if (link == null)
                {
                    throw new ArgumentException("Invalid Request!");
                }

                ViewData.Add("LinkArtifactRequirementID", new SelectList(
                        new List<LinkBetweenRequirementsArtifactsEntity>() { link }, "LinkArtifactRequirementID", "DisplayName", id));
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult GetAttributes(int LinkArtifactRequirementID)
        {
            try
            {
                return Json(service.filter(l => l.LinkArtifactRequirementID == LinkArtifactRequirementID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
