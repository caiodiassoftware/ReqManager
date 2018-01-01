using System;
using System.Web.Mvc;
using ReqManager.Entities.Artifact;
using ReqManager.ManagerController;
using ReqManager.Services.Project.Interfaces;
using ReqManager.Services.Acess.Interfaces;

namespace ReqManager.Controllers
{
    //ART2
    public class ProjectArtifactController : BaseController<ProjectArtifactEntity>
    {
        private IProjectArtifactService service { get; set; }

        public ProjectArtifactController(
            IProjectArtifactService service,
            IUserService userService,
            IArtifactTypeService typeService,
            IImportanceService measureService,
            IProjectService projectService) : base(service)
        {
            this.service = service;

            ViewData.Add("ArtifactTypeID", new SelectList(typeService.getAll(), "ArtifactTypeID", "description"));
            ViewData.Add("ImportanceID", new SelectList(measureService.getAll(), "ImportanceID", "description"));
            ViewData.Add("ProjectID", new SelectList(projectService.getAll(), "ProjectID", "description"));
            ViewData.Add("CreationUserID", new SelectList(userService.getAll(), "UserID", "name"));
        }

        public JsonResult GetWithCode(string code)
        {
            try
            {
                return Json(service.GetWithCode(code), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult GetArtifactsFromProject(int ProjectID)
        {
            try
            {
                return Json(service.getArtifactsByProject(ProjectID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [OutputCache(NoStore = true, Location = System.Web.UI.OutputCacheLocation.None)]
        public override ActionResult Edit(ProjectArtifactEntity entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.update(entity, getLoginUser());
                    success("Register has been successfully edited!");
                    return RedirectToAction("Index");
                }
                else
                {
                    getModelStateValidations();
                }

                return View(entity);
            }
            catch (Exception ex)
            {
                return filterException(ex);
            }
        }
    }
}
