using System.Web.Mvc;
using ReqManager.Entities.Link;
using ReqManager.ManagerController;
using ReqManager.Services.Estructure;
using ReqManager.Entities.Artifact;
using ReqManager.Entities.Requirement;
using ReqManager.Entities.Acess;
using ReqManager.Services.Artifact.Interfaces;
using System;
using System.Collections.Generic;
using ReqManager.Services.Link.Interfaces;
using ReqManager.Services.Requirements.Interfaces;
using System.Net;

namespace ReqManager.Controllers
{
    public class LinkBetweenRequirementsArtifactController : BaseController<LinkBetweenRequirementsArtifactsEntity>
    {
        private IArtifactRequirementTraceabilityMatrixService matrix { get; set; }
        private ILinkBetweenRequirementsArtifactsService linkService { get; set; }
        private IRequirementService requirementService { get; set; }

        public LinkBetweenRequirementsArtifactController(
            ILinkBetweenRequirementsArtifactsService linkService,
            IService<ProjectArtifactEntity> artifactService,
            IRequirementService requirementService,
            IService<UserEntity> userService,
            IService<TypeLinkEntity> typeService,
            IArtifactRequirementTraceabilityMatrixService matrix) : base(linkService)
        {
            this.matrix = matrix;
            this.linkService = linkService;
            this.requirementService = requirementService;

            ViewData.Add("ProjectArtifactID", new SelectList(artifactService.getAll(), "ProjectArtifactID", "code"));
            ViewData.Add("TypeLinkID", new SelectList(typeService.getAll(), "TypeLinkID", "description"));
        }

        #region GETS

        public JsonResult GetLink(string ArtifactCode, string RequirementCode)
        {
            try
            {
                return Json(linkService.get(ArtifactCode, RequirementCode), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult GetWithCode(string code)
        {
            try
            {
                return Json(linkService.getWithCode(code),
                    JsonRequestBehavior.AllowGet);
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

                ViewData.Add("RequirementID", new SelectList(
                        new List<RequirementEntity>() { reqOrigin }, "RequirementID", "DisplayName", id));
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult GetLinkArtifactsRequirementsFromProject(string ProjectID)
        {
            try
            {
                IEnumerable<RequirementEntity> requirements = null;

                HashSet<LinkBetweenRequirementsArtifactsEntity> links = new HashSet<LinkBetweenRequirementsArtifactsEntity>();

                foreach (RequirementEntity req in requirements)
                {
                    IEnumerable<LinkBetweenRequirementsArtifactsEntity> linksFilter = linkService.filter(l => l.RequirementID.Equals(req.RequirementID));
                    foreach (LinkBetweenRequirementsArtifactsEntity link in linksFilter)
                        links.Add(link);
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
