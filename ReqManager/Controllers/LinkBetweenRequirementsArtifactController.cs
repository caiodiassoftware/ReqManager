using System.Web.Mvc;
using ReqManager.Entities.Link;
using ReqManager.ManagerController;
using ReqManager.Services.Estructure;
using ReqManager.Entities.Artifact;
using ReqManager.Entities.Requirement;
using ReqManager.Entities.Acess;
using ReqManager.Services.Artifact.Interfaces;
using ReqManager.ViewModels;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using ReqManager.Services.Link.Interfaces;
using ReqManager.Services.Project.Interfaces;

namespace ReqManager.Controllers
{
    public class LinkBetweenRequirementsArtifactController : BaseController<LinkBetweenRequirementsArtifactsEntity>
    {
        private IArtifactRequirementTraceabilityMatrixService matrix { get; set; }
        private ILinkBetweenRequirementsArtifactsService linkService { get; set; }
        private IProjectRequirementsService projectRequirements { get; set; }

        public LinkBetweenRequirementsArtifactController(
            ILinkBetweenRequirementsArtifactsService linkService,
            IService<ProjectArtifactEntity> artifactService,
            IService<RequirementEntity> reqService,
            IService<UserEntity> userService,
            IService<TypeLinkEntity> typeService,
            IProjectRequirementsService projectRequirements,
            IArtifactRequirementTraceabilityMatrixService matrix) : base(linkService)
        {
            this.matrix = matrix;
            this.linkService = linkService;
            this.projectRequirements = projectRequirements;

            ViewData.Add("ProjectArtifactID", new SelectList(artifactService.getAll(), "ProjectArtifactID", "code"));
            ViewData.Add("RequirementID", new SelectList(reqService.getAll(), "RequirementID", "code"));
            ViewData.Add("TypeLinkID", new SelectList(typeService.getAll(), "TypeLinkID", "description"));
            ViewData.Add("UserID", new SelectList(userService.getAll(), "UserID", "name"));
        }

        public ActionResult ArtifactRequirementTraceabilityMatrix()
        {
            try
            {
                DataTableViewModel dt = new DataTableViewModel();
                dt.dataTable = matrix.getMatrix();
                return View(dt);
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
                IEnumerable<RequirementEntity> requirements =
                    projectRequirements.getRequirementsByProject(Convert.ToInt32(ProjectID)).Select(r => r.Requirement);
                
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

        public ActionResult ShowLink(string art, string req)
        {
            try
            {
                LinkBetweenRequirementsArtifactsEntity link = Service.getAll().
                    Where(l => l.Requirement.code.Equals(req) && l.ProjectArtifact.code.Equals(art)).SingleOrDefault();
                return RedirectToAction("Index", link.LinkArtifactRequirementID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
