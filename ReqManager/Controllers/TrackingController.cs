using ReqManager.Entities.Artifact;
using ReqManager.Entities.Project;
using ReqManager.Entities.Requirement;
using ReqManager.Filters;
using ReqManager.Services.Directories.Interfaces;
using ReqManager.Services.Link.Interfaces;
using ReqManager.Services.Project.Interfaces;
using ReqManager.Services.Requirements.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ReqManager.Controllers
{
    [Permissions]
    public class TrackingController : Controller
    {
        #region Propriets

        private IScanDirectoryService directory { get; set; }
        private IRequirementService requirement { get; set; }
        private IProjectArtifactService artifact { get; set; }
        private ILinkBetweenRequirementsService linkReq { get; set; }
        private ILinkBetweenRequirementsArtifactsService linkArt { get; set; }
        private IProjectService project { get; set; }
        private string path { get; set; }

        #endregion

        #region Constructor

        public TrackingController(
            IRequirementService requirement,
            IProjectArtifactService artifact,
            ILinkBetweenRequirementsService linkReq,
            ILinkBetweenRequirementsArtifactsService linkArt,
            IScanDirectoryService directory,
            IProjectService project)
        {
            this.directory = directory;
            this.requirement = requirement;
            this.artifact = artifact;
            this.linkReq = linkReq;
            this.linkArt = linkArt;
            this.project = project;
        }

        #endregion

        #region Requirements

        public ActionResult TrackingProjectRequirement()
        {
            try
            {
                ViewData.Add("Requirements", Enumerable.Empty<SelectListItem>());
                ViewData.Add("Project", new SelectList(project.getAll(), "ProjectID", "DisplayName"));

                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult TrackingRequirement(int? id)
        {
            try
            {
                RequirementEntity req = requirement.get(Convert.ToInt32(id));

                List<RequirementEntity> reqList = new List<RequirementEntity>()
                    {
                        req
                    };

                ViewBag.Title = "Track Project Requirement " + req.code;

                ViewData.Add("Requirements", new SelectList(reqList.AsEnumerable(), "RequirementID", "DisplayName", id));

                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult TrackingRequirements(string Requirements, string Path)
        {
            try
            {
                RequirementEntity req = requirement.get(Convert.ToInt32(Requirements));

                string[] requirements = { req.code };
                List<string> files = directory.findFile(requirements, Path);

                JsonResult json = Json(files, JsonRequestBehavior.AllowGet);
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Artifacts

        public ActionResult TrackingProjectArtifact()
        {
            try
            {
                ViewData.Add("ProjectID", new SelectList(project.getAll(), "ProjectID", "DisplayName"));

                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult TrackingArtifact(int? id)
        {
            try
            {
                ProjectArtifactEntity art = artifact.get(id);

                List<ProjectArtifactEntity> artList = new List<ProjectArtifactEntity>()
                    {
                        art
                    };

                List<ProjectEntity> prjList = new List<ProjectEntity>()
                    {
                        art.Project
                    };

                ViewBag.Title = "Track Project Requirement " + art.code;

                ViewData.Add("Artifacts", new SelectList(artList.AsEnumerable(), "ProjectArtifactID", "DisplayName", id));
                ViewData.Add("ProjectID", new SelectList(prjList.AsEnumerable(), "ProjectID", "DisplayName", art.ProjectID));
                ViewData.Add("Path", new SelectList(directory.getFolders(art.Project.pathForTraceability)));

                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult TrackingArtifacts(string item, string Path)
        {
            try
            {
                ProjectArtifactEntity art = artifact.get(Convert.ToInt32(item));

                string[] artifacts = { art.code };
                List<string> files = directory.findFile(artifacts, Path);

                JsonResult json = Json(files, JsonRequestBehavior.AllowGet);
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Link between Requirements

        public ActionResult TrackingLinkBetweenRequirement()
        {
            try
            {
                ViewData.Add("Project", new SelectList(project.getAll(), "ProjectID", "DisplayName"));
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult TrackingLinkBetweenRequirements(string Link, string Path)
        {
            try
            {
                string[] itens = { linkReq.get(Convert.ToInt32(Link)).code };
                List<string> files = directory.findFile(itens, Path);

                JsonResult json = Json(files, JsonRequestBehavior.AllowGet);
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Link between Requirements and Artifacts

        public ActionResult TrackingLinkBetweenRequirementArtifact()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult TrackingLinkBetweenRequirementArtifacts(string item, string Path)
        {
            try
            {
                string[] itens = { linkArt.get(Convert.ToInt32(item)).code };
                List<string> files = directory.findFile(itens, Path);

                JsonResult json = Json(files, JsonRequestBehavior.AllowGet);
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}