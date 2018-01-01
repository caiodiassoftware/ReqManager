using ReqManager.Entities.Requirement;
using ReqManager.ManagerController;
using ReqManager.Services.Requirements.Interfaces;
using System;
using System.Net;
using System.Web.Mvc;

namespace ReqManager.Controllers
{
    public class RequirementCharacteristicsController : BaseController<RequirementCharacteristicsEntity>
    {
        public RequirementCharacteristicsController(
            IRequirementCharacteristicsService service,
            IRequirementService requirement,
            ICharacteristicsService characteristics) : base(service)
        {
            ViewData.Add("CharacteristicsID", new SelectList(characteristics.getAll(), "CharacteristicsID", "name"));
        }

        public void Check(int RequirementCharacteristicsID, bool check)
        {
            try
            {
                RequirementCharacteristicsEntity entity = Service.get(RequirementCharacteristicsID);
                entity.check = check;
                Service.update(ref entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet, ActionName("Create")]
        public ActionResult CreateNewRequirementCharacterisc(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentException("Invalid Request!");
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public void CreateConfirmed(int? ID, int? CharacteristicsID, bool? check)
        {
            try
            {
                RequirementCharacteristicsEntity characteristic = new RequirementCharacteristicsEntity();
                characteristic.check = Convert.ToBoolean(check);
                characteristic.RequirementID = Convert.ToInt32(ID);
                characteristic.CharacteristicsID = Convert.ToInt32(CharacteristicsID);

                setCreationDate(ref characteristic);
                setIdUser(ref characteristic);

                if(TryValidateModel(characteristic))
                {
                    Service.add(ref characteristic);
                    ModelState.Clear();
                    success("Register was made with Success!");
                    Response.Redirect("~/RequirementCharacteristics/Create/" + ID);
                }
                else
                {
                    getModelStateValidations();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [OutputCache(NoStore = true, Location = System.Web.UI.OutputCacheLocation.None)]
        public override ActionResult DeleteConfirmed(int id)
        {
            var req = Service.get(id);
            int RequirementID = req.RequirementID;

            Service.delete(id);
            success("Registration has been successfully deleted!");
            return RedirectToAction("Details", "Requirement", new { id = RequirementID });
        }

        [HttpPost]
        [AllowAnonymous]
        [OutputCache(NoStore = true, Location = System.Web.UI.OutputCacheLocation.None)]
        public override ActionResult Edit(RequirementCharacteristicsEntity entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Service.update(ref entity);
                    success("Registration has been successfully edited!");
                    return RedirectToAction("Details", "Requirement", new { id = entity.RequirementID });
                }
                else
                {
                    getModelStateValidations();
                }

                return View(entity);
            }
            catch (Exception ex)
            {
                return filterException(ex);
            }
        }
    }
}
