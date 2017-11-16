using System.Web.Mvc;
using ReqManager.Entities.Link;
using ReqManager.ManagerController;
using ReqManager.Services.Requirements.Interfaces;
using ReqManager.Services.Link.Interfaces;
using ReqManager.Services.Acess.Interfaces;
using ReqManager.ViewModels;
using System;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.Collections.Generic;
using ReqManager.Services.Project.Interfaces;
using System.Linq;
using ReqManager.Entities.Requirement;

namespace ReqManager.Controllers
{
    public class LinkBetweenRequirementsController : BaseController<LinkBetweenRequirementsEntity>
    {
        private IRequirementTraceabilityMatrixService matrixService { get; set; }
        private ILinkBetweenRequirementsService linkService { get; set; }
        private IProjectRequirementsService projectRequirements { get; set; }

        public LinkBetweenRequirementsController(
            ILinkBetweenRequirementsService linkService,
            IRequirementService requirementService,
            IProjectRequirementsService projectRequirements,
            ITypeLinkService typeLinkService,
            IUserService userService,
            IRequirementTraceabilityMatrixService matrixService) : base(linkService)
        {
            this.matrixService = matrixService;
            this.linkService = linkService;
            this.projectRequirements = projectRequirements;

            ViewData.Add("RequirementOriginID", new SelectList(requirementService.getAll(), "RequirementID", "code"));
            ViewData.Add("RequirementTargetID", new SelectList(requirementService.getAll(), "RequirementID", "code"));
            ViewData.Add("TypeLinkID", new SelectList(typeLinkService.getAll(), "TypeLinkID", "description"));
            ViewData.Add("UserID", new SelectList(userService.getAll(), "UserID", "name"));
        }

        public void AddAttribute(int AttributeId, string value)
        {
            try
            {
                LinkRequirementAttributesEntity attribute = new LinkRequirementAttributesEntity()
                {
                    AttributeID = AttributeId,
                    value = value
                };

                List<LinkRequirementAttributesEntity> attributes = 
                    Session["LinkBetweenRequirementsController_Attributes"] == null ? 
                    new List<LinkRequirementAttributesEntity>() : 
                    Session["LinkBetweenRequirementsController_Attributes"] as List<LinkRequirementAttributesEntity>;

                attributes.Add(attribute);

                Session["LinkBetweenRequirementsController_Attributes"] = attributes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Create(LinkBetweenRequirementsEntity entity)
        {
            try
            {
                setIdUser(ref entity);

                if (ModelState.IsValid)
                {
                    List<LinkRequirementAttributesEntity> attributes = Session["LinkBetweenRequirementsController_Attributes"] == null ?
                        new List<LinkRequirementAttributesEntity>() : Session["LinkBetweenRequirementsController_Attributes"] as List<LinkRequirementAttributesEntity>;

                    linkService.add(entity, attributes);

                    ViewBag.MessageReqManager = String.Format("Register was made with Success!");
                    return RedirectToAction("Index");
                }
                else
                {
                    getModelStateValidations();
                }

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

        public ActionResult RequirementTraceabilityMatrix()
        {
            DataTableViewModel dt = new DataTableViewModel();
            dt.dataTable = matrixService.getMatrix();
            return View(dt);
        }

        public JsonResult GetLinkRequirementsFromProject(string ProjectID)
        {
            try
            {
                IEnumerable<RequirementEntity> requirements =
                    projectRequirements.getRequirementsByProject(Convert.ToInt32(ProjectID)).Select(r => r.Requirement);

                HashSet<LinkBetweenRequirementsEntity> links = new HashSet<LinkBetweenRequirementsEntity>();

                foreach (RequirementEntity req in requirements)
                {
                    IEnumerable<LinkBetweenRequirementsEntity> linksFilter = 
                        linkService.filter(l => l.RequirementOriginID.Equals(req.RequirementID) 
                        || l.RequirementTargetID.Equals(req.RequirementID));
                    foreach (LinkBetweenRequirementsEntity link in linksFilter)
                    {
                        links.Add(link);
                    }
                }

                return Json(links, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
