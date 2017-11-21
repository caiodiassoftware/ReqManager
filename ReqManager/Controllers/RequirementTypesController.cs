using ReqManager.Entities.Requirement;
using ReqManager.ManagerController;
using ReqManager.Services.Requirements.Interfaces;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Web.Mvc;

namespace ReqManager.Controllers
{
    public class RequirementTypesController : BaseController<RequirementTypeEntity>
    {
        private IRequirementTypeService typeService { get; set; }

        public RequirementTypesController(IRequirementTypeService typeService) : base(typeService)
        {
            this.typeService = typeService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Create(RequirementTypeEntity entity)
        {
            try
            {
                setIdUser(ref entity);
                if (ModelState.IsValid)                
                    typeService.add(ref entity);
                else                
                    getModelStateValidations();
                return View();
            }
            catch (DbEntityValidationException ex)
            {
                return getMessageDbValidation(entity, ex);
            }
            catch (DbUpdateException ex)
            {
                return getMessageDbUpdateException(entity, ex);
            }
            catch (Exception ex)
            {
                return getMessageGeralException(entity, ex);
            }
        }
    }
}
