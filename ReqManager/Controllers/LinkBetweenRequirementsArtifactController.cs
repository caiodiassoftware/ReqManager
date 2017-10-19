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

namespace ReqManager.Controllers
{
    public class LinkBetweenRequirementsArtifactController : BaseController<LinkBetweenRequirementsArtifactsEntity>
    {
        private IArtifactRequirementTraceabilityMatrixService matrix { get; set; }

        public LinkBetweenRequirementsArtifactController(
            IService<LinkBetweenRequirementsArtifactsEntity> service,
            IService<ProjectArtifactEntity> artifactService,
            IService<RequirementEntity> reqService,
            IService<UserEntity> userService,
            IService<TypeLinkEntity> typeService,
            IArtifactRequirementTraceabilityMatrixService matrix) : base(service)
        {
            this.matrix = matrix;
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
