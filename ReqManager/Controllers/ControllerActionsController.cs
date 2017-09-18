using ReqManager.ManagerController;
using System.Web.Mvc;
using ReqManager.Services.InterfacesServices;
using System.Reflection;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ReqManager.Controllers
{
    public class ControllerActionsController : BaseController<Entities.ControllerActionEntity>
    {
        public ControllerActionsController(IControllerActionService service) : base(service)
        {

        }

        public ActionResult Refresh()
        {
            try
            {
                return null;
            //    List<ControllerAction> controllerActions = new List<ControllerAction>();
            //    Assembly asm = Assembly.GetAssembly(typeof(MvcApplication));

            //    List<Type> controlleractionlist = asm.GetTypes().Where(type => typeof(System.Web.Mvc.Controller).IsAssignableFrom(type)).
            //        Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any()).ToList();

            //    List<ControllerAction> baseControllerMethods = controlleractionlist.Where(x => x.Name.Contains("BaseController")).FirstOrDefault().
            //        GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public).
            //        Where(m => m.IsVirtual && !m.DeclaringType.Equals(m)).Select(m =>
            //        new ControllerAction
            //        {
            //            action = m.Name,
            //            controller = m.DeclaringType.Name,
            //            IsGet = m.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", "")).Contains("HttpPost") ? false : true
            //        }).ToList();

            //    foreach (var item in controlleractionlist.Where(x => !x.Name.Contains("BaseController")))
            //    {
            //        controllerActions.AddRange(item.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public).
            //            Where(m => m.DeclaringType.Equals(item)).Select(m =>
            //            new ControllerAction
            //            {
            //                action = m.Name,
            //                controller = m.DeclaringType.Name,
            //                IsGet = m.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", "")).Contains("HttpGet") ? false : true
            //            }));

            //        if (item.BaseType.Name.Contains("BaseController"))
            //        {
            //            baseControllerMethods.ForEach(m => m.controller = item.Name);
            //            controllerActions.AddRange(baseControllerMethods.Select(x =>
            //            new ControllerAction
            //            {
            //                action = x.action,
            //                controller = x.controller,
            //                IsGet = x.IsGet
            //            }));
            //        }
            //    }

            //((IControllerActionService)Service).Refresh(controllerActions);
            //    Service.saveChanges();
            //    return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
