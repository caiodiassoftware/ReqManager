using ReqManager.Entities.Artifact;
using ReqManager.Entities.Link;
using ReqManager.Entities.Requirement;
using ReqManager.ManagerController;
using ReqManager.Services.Directories.Interfaces;
using ReqManager.Services.Link.Classes;
using ReqManager.Services.Link.Interfaces;
using ReqManager.Services.Project.Interfaces;
using ReqManager.Services.Requirements.Interfaces;
using ReqManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReqManager.Controllers
{
    public class TrackingController : ControlAcessController<TrackingViewModel>
    {
        #region Propriets

        private IScanDirectoryService directory { get; set; }
        private IRequirementService requirement { get; set; }
        private IProjectArtifactService artifact { get; set; }
        private ILinkBetweenRequirementsService linkReq { get; set; }
        private ILinkBetweenRequirementsArtifactsService linkArt { get; set; }
        private IProjectRequirementsService reqProj { get; set; }
        private string path { get; set; }

        #endregion

        #region Construtor

        public TrackingController(
            IRequirementService requirement,
            IProjectArtifactService artifact,
            ILinkBetweenRequirementsService linkReq,
            ILinkBetweenRequirementsArtifactsService linkArt,
            IScanDirectoryService directory,
            IProjectRequirementsService reqProj)
        {
            this.reqProj = reqProj;
            this.directory = directory;
            this.requirement = requirement;
            this.artifact = artifact;
            this.linkReq = linkReq;
            this.linkArt = linkArt;
        }

        #endregion

        #region Requirements

        public ActionResult TrackingRequirement(int? id)
        {
            try
            {
                RequirementEntity req = requirement.get(Convert.ToInt32(id));
                path = reqProj.getAll().Where(pr => pr.RequirementID.Equals(req.RequirementID)).FirstOrDefault().Project.pathForTraceability;

                ViewData.Add("Requirements", new SelectList(requirement.getAll(), "RequirementID", "DisplayName"));
                ViewData.Add("Path", new SelectList(directory.getFolders(path)));

                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult TrackingRequirements(string item, string Path)
        {
            try
            {
                RequirementEntity req = requirement.get(Convert.ToInt32(item));

                string[] requirements = { req.code };
                List<string> files = directory.findFile(requirements, Path);

                return PartialView("~/Views/Shared/List.cshtml", files);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Artifacts

        public ActionResult TrackingArtifact(int? id)
        {
            try
            {
                ProjectArtifactEntity art = artifact.get(id);

                ViewData.Add("Artifacts", new SelectList(artifact.getAll(), "ProjectArtifactID", "DisplayName"));
                ViewData.Add("Path", new SelectList(directory.getFolders(art.Project.pathForTraceability)));

                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult TrackingArtifacts(string item, string Path)
        {
            try
            {
                string[] itens = { artifact.get(Convert.ToInt32(item)).code };
                List<string> files = directory.findFile(itens, Path);

                return PartialView("~/Views/Shared/List.cshtml", files);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Link between Requirements

        public ActionResult TrackingLinkBetweenRequirement(int? id)
        {
            try
            {
                LinkBetweenRequirementsEntity link = linkReq.get(id);

                string pathOrigin = reqProj.getAll().Where(pr => pr.RequirementID.Equals(link.RequirementOrigin.RequirementID)).FirstOrDefault().Project.pathForTraceability;
                string pathTarget = reqProj.getAll().Where(pr => pr.RequirementID.Equals(link.RequirementTarget.RequirementID)).FirstOrDefault().Project.pathForTraceability;

                List<string> paths = new List<string>();
                paths.AddRange(directory.getFolders(pathOrigin));
                paths.AddRange(directory.getFolders(pathTarget));

                HashSet<string> hashPath = new HashSet<string>();
                paths.ForEach(p => hashPath.Add(p));

                ViewData.Add("LinkBetweenRequirement", new SelectList(linkReq.getAll(), "LinkRequirementsID", "DisplayName"));
                ViewData.Add("Path", new SelectList(hashPath));

                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult TrackingLinkBetweenRequirements(string item, string Path)
        {
            try
            {
                string[] itens = { linkReq.get(Convert.ToInt32(item)).code };
                List<string> files = directory.findFile(itens, Path);

                return PartialView("~/Views/Shared/List.cshtml", files);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Link between Requirements and Artifacts

        public ActionResult TrackingLinkBetweenRequirementArtifact(int? id)
        {
            try
            {
                LinkBetweenRequirementsArtifactsEntity art = linkArt.get(id);

                string artPath = art.ProjectArtifact.path;
                string pathOrigin = reqProj.getAll().Where(pr => pr.RequirementID.Equals(art.Requirement.RequirementID)).FirstOrDefault().Project.pathForTraceability;

                List<string> paths = new List<string>();
                paths.AddRange(directory.getFolders(pathOrigin));
                paths.AddRange(directory.getFolders(artPath));

                HashSet<string> hashPath = new HashSet<string>();
                paths.ForEach(p => hashPath.Add(p));

                ViewData.Add("LinkBetweenArtifactRequirement", new SelectList(linkArt.getAll(), "LinkArtifactRequirementID", "DisplayName"));
                ViewData.Add("Path", new SelectList(hashPath));

                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult TrackingLinkBetweenRequirementArtifacts(string item, string Path)
        {
            try
            {
                string[] itens = { linkArt.get(Convert.ToInt32(item)).code };
                List<string> files = directory.findFile(itens, Path);

                return PartialView("~/Views/Shared/List.cshtml", files);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Others

        public void OpenFile(string file)
        {
            try
            {
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.EnableRaisingEvents = false;
                proc.StartInfo.FileName = file;
                proc.Start();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}