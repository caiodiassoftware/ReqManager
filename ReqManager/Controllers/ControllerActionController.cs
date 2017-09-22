﻿using AutoMapper;
using ReqManager.Entities;
using ReqManager.ManagerController;
using ReqManager.Services.Estructure;
using ReqManager.Services.InterfacesServices;
using ReqManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using ReqManager.Utils.Extensions;

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
                List<ControllerActionViewModel> controllerActions = new List<ControllerActionViewModel>();
                Assembly asm = Assembly.GetAssembly(typeof(MvcApplication));

                List<Type> controlleractionlist = asm.GetTypes().Where(type => typeof(Controller).IsAssignableFrom(type)).
                    Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any()).ToList();

                List<ControllerActionViewModel> baseControllerMethods = controlleractionlist.Where(x => x.Name.Contains("BaseController")).FirstOrDefault().
                    GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public).
                    Where(m => m.IsVirtual && !m.DeclaringType.Equals(m)).Select(m =>
                    new ControllerActionViewModel
                    {
                        Action = m.Name,
                        Controller = m.DeclaringType.Name,
                        IsGet = m.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", "")).Contains("HttpPost") ? false : true
                    }).ToList();

                foreach (var item in controlleractionlist.Where(x => !x.Name.Contains("BaseController")))
                {
                    controllerActions.AddRange(item.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public).
                        Where(m => m.DeclaringType.Equals(item)).Select(m =>
                        new ControllerActionViewModel
                        {
                            Action = m.Name,
                            Controller = m.DeclaringType.Name,
                            IsGet = m.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", "")).Contains("HttpGet") ? false : true
                        }));

                    if (item.BaseType.Name.Contains("BaseController"))
                    {
                        baseControllerMethods.ForEach(m => m.Controller = item.Name);
                        controllerActions.AddRange(baseControllerMethods.Select(x =>
                        new ControllerActionViewModel
                        {
                            Action = x.Action,
                            Controller = x.Controller,
                            IsGet = x.IsGet
                        }));
                    }
                }

                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<ControllerActionViewModel, ControllerActionEntity>();
                    cfg.IgnoreUnmapped();
                });

                IEnumerable<ControllerActionEntity> controllerActionApplication = Mapper.Map<IEnumerable<ControllerActionViewModel>, IEnumerable<ControllerActionEntity>>(controllerActions);

                service.Refresh(controllerActionApplication);
                Service.saveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}