using System.Web.Mvc;
using ReqManager.Entities.Link;
using ReqManager.ManagerController;
using ReqManager.Services.Requirements.Interfaces;
using ReqManager.Services.Link.Interfaces;
using ReqManager.Services.Acess.Interfaces;
using System;
using System.Collections.Generic;
using ReqManager.Entities.Requirement;
using System.Net;

namespace ReqManager.Controllers
{
    public class LinkBetweenRequirementsController : BaseController<LinkBetweenRequirementsEntity>
    {
        private IRequirementTraceabilityMatrixService matrixService { get; set; }
        private ILinkBetweenRequirementsService linkService { get; set; }
        private IRequirementService requirementService { get; set; }

        public LinkBetweenRequirementsController(
            ILinkBetweenRequirementsService linkService,
            IRequirementService requirementService,
            ITypeLinkService typeLinkService,
            IUserService userService,
            IRequirementTraceabilityMatrixService matrixService) : base(linkService)
        {
            this.matrixService = matrixService;
            this.linkService = linkService;
            this.requirementService = requirementService;

            ViewData.Add("RequirementOriginID", new SelectList(requirementService.getAll(), "RequirementID", "code"));
            ViewData.Add("RequirementTargetID", new SelectList(requirementService.getAll(), "RequirementID", "code"));
            ViewData.Add("TypeLinkID", new SelectList(typeLinkService.getAll(), "TypeLinkID", "description"));
        }

        #region GETS

        public JsonResult GetWithCode(string code)
        {
            try
            {
                return Json(linkService.getWithCode(code),
                    JsonRequestBehavior.AllowGet); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult CreateNewLink(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                RequirementEntity reqOrigin = requirementService.get(id);

                if (reqOrigin == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                ViewData.Add("RequirementOriginID", new SelectList(
                        new List<RequirementEntity>() { reqOrigin }, "RequirementID", "DisplayName", id));
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        public JsonResult GetLink(string ReqOrigin, string ReqTarget)
        {
            try
            {
                return Json(linkService.get(ReqOrigin, ReqTarget), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult GetLinkRequirementsFromProject(string ProjectID)
        {
            try
            {
                IEnumerable<RequirementEntity> requirements = null;

                HashSet<LinkBetweenRequirementsEntity> links = new HashSet<LinkBetweenRequirementsEntity>();

                foreach (RequirementEntity req in requirements)
                {
                    IEnumerable<LinkBetweenRequirementsEntity> linksFilter =
                        linkService.filter(l => l.RequirementOriginID.Equals(req.RequirementID)
                        || l.RequirementTargetID.Equals(req.RequirementID));
                    foreach (LinkBetweenRequirementsEntity link in linksFilter)
                    {
                        links.Add(link);
                    }
                }

                return Json(links, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
