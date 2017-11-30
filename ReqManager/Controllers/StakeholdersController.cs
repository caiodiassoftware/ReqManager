using System.Web.Mvc;
using ReqManager.Entities.Project;
using ReqManager.ManagerController;
using ReqManager.Services.Project.Interfaces;
using ReqManager.Services.Acess.Interfaces;

namespace ReqManager.Views
{
    public class StakeholdersController : BaseController<StakeholdersEntity>
    {
        public StakeholdersController(IStakeholdersService service, IStakeholderClassificationService classService, IUserService userService) : base(service)
        {
            ViewBag.CreationUserID = new SelectList(userService.getAll(), "UserID", "name");
            ViewBag.ClassificationID = new SelectList(classService.getAll(), "ClassificationID", "description");
        }
    }
}
