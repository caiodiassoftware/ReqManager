using AutoMapper;
using ReqManager.Entities;
using ReqManager.ManagerController;
using ReqManager.Services.Extensions;
using ReqManager.Services.InterfacesServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace ReqManager.Controllers
{
    public class ControllerActionController : BaseController<ControllerActionEntity>
    {
        private IControllerActionService service { get; set; }

        public ControllerActionController(IControllerActionService service) : base(service)
        {
            this.service = service;
        }

        public ActionResult Refresh()
        {
            try
            {
                List<ControllerActionEntity> controllerActionsToDataBase = new List<ControllerActionEntity>();
                Assembly asm = Assembly.GetAssembly(typeof(MvcApplication));

                List<Type> controllerActionsFromApplication = asm.GetTypes().Where(type => typeof(Controller).IsAssignableFrom(type)).
                    Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any()).ToList();

                List<ControllerActionEntity> baseControllerMethods = new List<ControllerActionEntity>();
                baseControllerMethods.AddRange(getActionsFromController(controllerActionsFromApplication, "BaseController"));
                baseControllerMethods.AddRange(getActionsFromController(controllerActionsFromApplication, "ControlAccessController"));

                foreach (var item in controllerActionsFromApplication.Where(
                    x => !x.Name.Contains("BaseController") &&
                    !x.Name.Contains("ControlAccessController")))
                {
                    controllerActionsToDataBase.AddRange(item.GetMethods(BindingFlags.Instance |
                        BindingFlags.DeclaredOnly | BindingFlags.Public).
                        Where(m => m.DeclaringType.Equals(item)).Select(m =>
                        new ControllerActionEntity
                        {
                            action = m.Name,
                            controller = m.DeclaringType.Name,
                            IsGet = m.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", "")).Contains("HttpGet") ? false : true
                        }));

                    if (item.BaseType.Name.Contains("BaseController") || item.BaseType.Name.Contains("ControlAccessController"))
                    {
                        baseControllerMethods.ForEach(m => m.controller = item.Name);
                        controllerActionsToDataBase.AddRange(baseControllerMethods.Select(x =>
                        new ControllerActionEntity
                        {
                            action = x.action,
                            controller = x.controller,
                            IsGet = x.IsGet
                        }));
                    }
                }

                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<ControllerActionEntity, ControllerActionEntity>();
                    cfg.IgnoreUnmapped();
                });

                IEnumerable<ControllerActionEntity> controllerActionApplication = Mapper.Map<IEnumerable<ControllerActionEntity>, IEnumerable<ControllerActionEntity>>(controllerActionsToDataBase);

                service.Refresh(controllerActionApplication);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<ControllerActionEntity> getActionsFromController(List<Type> actionsList, string controllerName)
        {
            return actionsList.Where
                                (x => x.Name.Contains(controllerName)).FirstOrDefault().
                                GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public).
                                Where(m => m.IsVirtual && !m.DeclaringType.Equals(m)).Select(m =>
                                new ControllerActionEntity
                                {
                                    action = m.Name,
                                    controller = m.DeclaringType.Name,
                                    IsGet = m.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", "")).Contains("HttpPost") ? false : true
                                }).ToList();
        }
    }
}
