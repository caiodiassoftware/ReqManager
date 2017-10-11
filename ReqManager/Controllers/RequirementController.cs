using System.Web.Mvc;
using ReqManager.Entities.Requirement;
using ReqManager.ManagerController;
using ReqManager.Services.Requirements.Interfaces;
using ReqManager.Services.Project.Interfaces;
using ReqManager.Services.Acess.Interfaces;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System;
using ReqManager.Services.Link.Interfaces;
using AutoMapper;
using ReqManager.Services.Extensions;
using ReqManager.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;

namespace ReqManager.Controllers
{
    //REQ1
    //R-R1
    //R-A1
    //PRJ4
    public class RequirementController : ControlAcessController<RequirementEntity>
    {
        private IRequirementRationaleService rationaleService { get; set; }
        private IRequirementActionHistoryService reqActionHistoryService { get; set; }
        private ILinkBetweenRequirementsService linkRequirementService { get; set; }
        private ILinkBetweenRequirementsArtifactsService linkReqArtifactService { get; set; }
        private IRequirementService Service { get; set; }

        public RequirementController(
            IRequirementService service,
            IMeasureImportanceService measureService,
            IRequirementStatusService statusService,
            IRequirementTypeService typeService,
            IUserService userService,
            IStakeholdersProjectService stakeholderProjectService,
            IRequirementRationaleService rationaleService,
            IRequirementActionHistoryService reqActionHistoryService,
            ILinkBetweenRequirementsService linkRequirementService,
            ILinkBetweenRequirementsArtifactsService linkReqArtifactService)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateAutomaticMapping<RequirementViewModel, RequirementEntity>();
                cfg.CreateAutomaticMapping<RequirementEntity, RequirementViewModel>();
            });

            Service = service;
            this.linkRequirementService = linkRequirementService;
            this.linkReqArtifactService = linkReqArtifactService;
            this.rationaleService = rationaleService;
            this.reqActionHistoryService = reqActionHistoryService;

            ViewData.Add("StakeholdersProjectID", new SelectList(stakeholderProjectService.getAll(), "StakeholdersProjectID", "DisplayName"));
            ViewData.Add("MeasureImportanceID", new SelectList(measureService.getAll(), "MeasureImportanceID", "description"));
            ViewData.Add("RequirementStatusID", new SelectList(statusService.getAll(), "RequirementStatusID", "description"));
            ViewData.Add("RequirementTypeID", new SelectList(typeService.getAll(), "RequirementTypeID", "description"));
            ViewData.Add("UserID", new SelectList(userService.getAll(), "UserID", "name"));
        }

        #region GETS

        public ActionResult Index()
        {
            try
            {
                return View(Mapper.Map<IEnumerable<RequirementEntity>, IEnumerable<RequirementViewModel>>(Service.getAll()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Details(int? id)
        {
            try
            {
                RequirementViewModel vm = Mapper.Map<RequirementEntity, RequirementViewModel>(Service.get(id));
                vm.linkReq = linkRequirementService.getAll().Where(r => r.RequirementOriginID.Equals(id) || r.RequirementTargetID.Equals(id)).ToList();
                vm.linkReqArt = linkReqArtifactService.getAll().Where(r => r.RequirementID.Equals(id)).ToList();

                return View(vm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Create()
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

        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RequirementEntity entity = Service.get(id);
                RequirementViewModel vm = Mapper.Map<RequirementEntity, RequirementViewModel>(entity);
                if (vm == null)
                {
                    return HttpNotFound();
                }
                return View(vm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RequirementEntity entity = Service.get(id);
                RequirementViewModel vm = Mapper.Map<RequirementEntity, RequirementViewModel>(entity);
                if (vm == null)
                {
                    return HttpNotFound();
                }
                return View(vm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RequirementViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RequirementEntity entity = Mapper.Map<RequirementViewModel, RequirementEntity>(vm);
                    setIdUser(ref entity);
                    Service.add(entity);
                    Service.saveChanges();
                    ViewBag.MessageReqManager = String.Format("Register was made with Success!");
                    return RedirectToAction("Index");
                }

                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RequirementViewModel vm)
        {
            try
            {
                RequirementEntity entity = Mapper.Map<RequirementViewModel, RequirementEntity>(vm);

                if (ModelState.IsValid)
                {
                    Service.update(entity);

                    RequirementActionHistoryEntity reqAction = new RequirementActionHistoryEntity();
                    RequirementEntity req = Service.get(entity.RequirementID);
                    reqAction.RequirementID = req.RequirementID;
                    reqAction.DescriptionStatus = req.RequirementStatus.description;
                    reqAction.UserLogin = getLoginUser();

                    reqActionHistoryService.add(reqAction);

                    Service.saveChanges();
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
                throw ex;
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Service.delete(id);
                Service.saveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.MessageReqManager = String.Format("Error Detected! " + ex.Message);
                return RedirectToAction("Index");
            }
        }

        #endregion
    }
}
