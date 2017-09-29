using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReqManager.Data.DataAcess;
using ReqManager.Entities.Link;
using ReqManager.ManagerController;
using ReqManager.Services.Estructure;

namespace ReqManager.Controllers
{
    public class LinkRequirementAttributesController : BaseController<LinkRequirementAttributesEntity>
    {
        private IService<LinkRequirementAttributesEntity> service { get; set; }
        private IService<AttributesEntity> attributeService { get; set; }
        private IService<LinkBetweenRequirementsEntity> linkRequirementsService { get; set; }

        public LinkRequirementAttributesController(
            IService<LinkRequirementAttributesEntity> service,
            IService<AttributesEntity> attributeService,
            IService<LinkBetweenRequirementsEntity> linkRequirementsService) : base(service)
        {
            this.service = service;
            this.attributeService = attributeService;
            this.linkRequirementsService = linkRequirementsService;
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

        private ActionResult dropDowns(LinkRequirementAttributesEntity entity = null)
        {
            ViewBag.AttributeID = new SelectList(attributeService.getAll(), "AttributeID", "description");
            ViewBag.LinkRequirementsID = new SelectList(linkRequirementsService.getAll(), "LinkRequirementsID", "code");
            return entity == null ? View() : View(entity);
        }

        #endregion
    }
}
