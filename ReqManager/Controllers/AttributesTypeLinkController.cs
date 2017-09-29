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
using ReqManager.Services.Link.Interfaces;

namespace ReqManager.Controllers
{
    public class AttributesTypeLinkController : BaseController<AttributesTypeLinkEntity>
    {
        private IAttributesTypeLinkService service { get; set; }
        private IAttributesService attributeService { get; set; }
        private ITypeLinkService typeLinkService { get; set; }

        public AttributesTypeLinkController(
            IAttributesTypeLinkService service,
            IAttributesService attributeService,
            ITypeLinkService typeLinkService) : base(service)
        {
            this.service = service;
            this.attributeService = attributeService;
            this.typeLinkService = typeLinkService;
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

        #region POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Create(AttributesTypeLinkEntity AttributesTypeLinkEntityEntity)
        {
            base.Create(AttributesTypeLinkEntityEntity);
            return dropDowns(AttributesTypeLinkEntityEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Edit(AttributesTypeLinkEntity AttributesTypeLinkEntityEntity)
        {
            base.Edit(AttributesTypeLinkEntityEntity);
            return dropDowns(AttributesTypeLinkEntityEntity);
        }

        #endregion

        #region Private Methods

        private ActionResult dropDowns(AttributesTypeLinkEntity entity = null)
        {
            ViewBag.AttributeID = new SelectList(attributeService.getAll(), "AttributeID", "description");
            ViewBag.TypeLinkID = new SelectList(typeLinkService.getAll(), "TypeLinkID", "description");
            return entity == null ? View() : View(entity);
        }

        #endregion

    }
}
