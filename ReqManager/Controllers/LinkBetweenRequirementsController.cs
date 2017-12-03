using System.Web.Mvc;
using ReqManager.Entities.Link;
using ReqManager.ManagerController;
using ReqManager.Services.Requirements.Interfaces;
using ReqManager.Services.Link.Interfaces;
using ReqManager.Services.Acess.Interfaces;
using ReqManager.ViewModels;
using System;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.Collections.Generic;
using ReqManager.Services.Project.Interfaces;
using System.Linq;
using ReqManager.Entities.Requirement;

namespace ReqManager.Controllers
{
    public class LinkBetweenRequirementsController : BaseController<LinkBetweenRequirementsEntity>
    {
        private IRequirementTraceabilityMatrixService matrixService { get; set; }
        private ILinkBetweenRequirementsService linkService { get; set; }

        public LinkBetweenRequirementsController(
            ILinkBetweenRequirementsService linkService,
            IRequirementService requirementService,
            ITypeLinkService typeLinkService,
            IUserService userService,
            IRequirementTraceabilityMatrixService matrixService) : base(linkService)
        {
            this.matrixService = matrixService;
            this.linkService = linkService;

            ViewData.Add("RequirementOriginID", new SelectList(requirementService.getAll(), "RequirementID", "code"));
            ViewData.Add("RequirementTargetID", new SelectList(requirementService.getAll(), "RequirementID", "code"));
            ViewData.Add("TypeLinkID", new SelectList(typeLinkService.getAll(), "TypeLinkID", "description"));
            ViewData.Add("CreationUserID", new SelectList(userService.getAll(), "UserID", "name"));
        }

        public ActionResult RequirementTraceabilityMatrix()
        {
            DataTableViewModel dt = new DataTableViewModel();
            //dt.dataTable = matrixService.getMatrix();
            return View(dt);
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

    }
}
