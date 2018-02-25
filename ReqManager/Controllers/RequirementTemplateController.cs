using System.Web.Mvc;
using ReqManager.Entities.Requirement;
using ReqManager.ManagerController;
using ReqManager.Services.Requirements.Interfaces;
using ReqManager.Services.Acess.Interfaces;
using ReqManager.ViewModels;
using System;
using System.Net;
using AutoMapper;
using ReqManager.Services.Extensions;
using System.Collections.Generic;

namespace ReqManager.Controllers
{
    public class RequirementTemplateController : ControlAccessController<RequirementTemplateEntity>
    {
        private IRequirementTemplateService service { get; set; }

        public RequirementTemplateController
            (IRequirementTemplateService service, IUserService userService, IRequirementTypeService type) : base(service)
        {
            this.service = service;

            Mapper.Initialize(cfg =>
            {
                cfg.CreateAutomaticMapping<RequirementTemplateViewModel, RequirementTemplateEntity>();
                cfg.CreateAutomaticMapping<RequirementTemplateEntity, RequirementTemplateViewModel>();
            });

            ViewData.Add("CreationUserID", new SelectList(userService.getAll(), "UserID", "DisplayName"));
            ViewData.Add("RequirementTypeID", new SelectList(type.getAll(), "RequirementTypeID", "description"));
        }

        #region GETS

        public JsonResult GetTemplatesOfRequirementType(int type)
        {
            try
            {
                JsonResult json = Json(service.filterByRequirementType(type), JsonRequestBehavior.AllowGet);
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult GetTemplateHtml(int id)
        {
            try
            {
                return Json(Service.get(id), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Index()
        {
            try
            {
                return View(Mapper.Map<IEnumerable<RequirementTemplateEntity>, IEnumerable<RequirementTemplateViewModel>>(Service.getAll()));
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
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                RequirementTemplateEntity entity = Service.get(id);
                RequirementTemplateViewModel vm = Mapper.Map<RequirementTemplateEntity, RequirementTemplateViewModel>(entity);
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
                RequirementTemplateEntity entity = Service.get(id);
                RequirementTemplateViewModel vm = Mapper.Map<RequirementTemplateEntity, RequirementTemplateViewModel>(entity);
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
                RequirementTemplateEntity entity = Service.get(id);
                RequirementTemplateViewModel vm = Mapper.Map<RequirementTemplateEntity, RequirementTemplateViewModel>(entity);
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
        [AllowAnonymous]
        [OutputCache(NoStore = true, Location = System.Web.UI.OutputCacheLocation.None)]
        public ActionResult Create(RequirementTemplateViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RequirementTemplateEntity entity = Mapper.Map<RequirementTemplateViewModel, RequirementTemplateEntity>(vm);
                    setIdUser(ref entity);
                    setCreationDate(ref entity);
                    Service.add(ref entity);
                    success("Register was made with Success!");
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
        [AllowAnonymous]
        [OutputCache(NoStore = true, Location = System.Web.UI.OutputCacheLocation.None)]
        public ActionResult Edit(RequirementTemplateViewModel vm)
        {
            try
            {
                RequirementTemplateEntity entity = new RequirementTemplateEntity();

                if (ModelState.IsValid)
                {
                    entity = Mapper.Map<RequirementTemplateViewModel, RequirementTemplateEntity>(vm);
                    Service.update(ref entity);
                    success("Register has been successfully edited!");
                    return RedirectToAction("Index");
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
        [AllowAnonymous]
        [OutputCache(NoStore = true, Location = System.Web.UI.OutputCacheLocation.None)]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Service.delete(id);
                success("Registration has been successfully deleted!");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
