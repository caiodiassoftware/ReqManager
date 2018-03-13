using System;
using System.Web.Mvc;
using ReqManager.Entities.Artifact;
using ReqManager.ManagerController;
using ReqManager.Services.Project.Interfaces;
using ReqManager.Services.Acess.Interfaces;
using System.Web;
using System.IO;

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

        [HttpPost]
        public ActionResult CreateNewArtifact(ProjectArtifactEntity entity)
        {
            try
            {
                if(Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        string directory = Server.MapPath("~/Uploads/Artifacts/");
                        if (!Directory.Exists(directory))
                            Directory.CreateDirectory(directory);
                        string path = Path.Combine(Server.MapPath("~/Uploads/Artifacts/"), fileName);
                        entity.path = path;

                        if (ModelState.IsValid)
                        {
                            base.Create(entity);
                            file.SaveAs(path);
                        }
                        else
                        {
                            getModelStateValidations();
                        }
                    }
                    else
                    {
                        warning("Empty File!");
                        return View();
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return filterException(ex);
            }
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
        public ActionResult EditArtifact(ProjectArtifactEntity entity)
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
