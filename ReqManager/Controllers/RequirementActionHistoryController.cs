using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReqManager.Data.DataAcess;
using ReqManager.Entities.Requirement;
using ReqManager.ManagerController;
using ReqManager.Services.Estructure;
using ReqManager.Services.Requirements.Interfaces;

namespace ReqManager.Controllers
{
    public class RequirementActionHistoryController : BaseController<RequirementActionHistoryEntity>
    {
        private IRequirementActionHistoryService service { get; set; }
        private IRequirementService reqService { get; set; }

        public RequirementActionHistoryController(IRequirementActionHistoryService service, IRequirementService reqService) : base(service)
        {
            this.service = service;
            this.reqService = reqService;
        }

        #region GETS

        public override ActionResult Create()
        {
            return dropDowns();
        }

        public override ActionResult Edit(int? id)
        {
            base.Edit(id);
            return dropDowns(Service.get(id));
        }

        public override ActionResult Delete(int? id)
        {
            base.Delete(id);
            return dropDowns(Service.get(id));
        }

        #endregion        

        #region Private Methods

        private ActionResult dropDowns(RequirementActionHistoryEntity entity = null)
        {
            ViewBag.RequirementID = new SelectList(reqService.getAll(), "RequirementID", "code");
            return entity == null ? View() : View(entity);
        }

        #endregion

    }
}
