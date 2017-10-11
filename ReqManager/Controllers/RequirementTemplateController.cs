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
using System.Linq;

namespace ReqManager.Controllers
{
    public class RequirementTemplateController : ControlAcessController<RequirementTemplateEntity>
    {
        private IRequirementTemplateService Service { get; set; }

        public RequirementTemplateController(IRequirementTemplateService service, IUserService userService, IRequirementTypeService type)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateAutomaticMapping<RequirementTemplateViewModel, RequirementTemplateEntity>();
                cfg.CreateAutomaticMapping<RequirementTemplateEntity, RequirementTemplateViewModel>();
            });

            Service = service;
            ViewData.Add("UserID", new SelectList(userService.getAll(), "UserID", "DisplayName"));
            ViewData.Add("RequirementTypeID", new SelectList(type.getAll(), "RequirementTypeID", "description"));
        }

        #region GETS

        public JsonResult GetTemplatesOfRequirementType(int type)
        {
            try
            {
                return Json(Service.getAll().Where(t => t.RequirementType.RequirementTypeID.Equals(type)).Select(t => t.RequirementTemplateID), JsonRequestBehavior.AllowGet);
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
        [ValidateAntiForgeryToken]
        public ActionResult Create(RequirementTemplateViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RequirementTemplateEntity entity = Mapper.Map<RequirementTemplateViewModel, RequirementTemplateEntity>(vm);
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
        public ActionResult Edit(RequirementTemplateViewModel vm)
        {
            try
            {
                RequirementTemplateEntity entity = new RequirementTemplateEntity();

                if (ModelState.IsValid)
                {
                    entity = Mapper.Map<RequirementTemplateViewModel, RequirementTemplateEntity>(vm);
                    Service.update(entity);
                    Service.saveChanges();
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
