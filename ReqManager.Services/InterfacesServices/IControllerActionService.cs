using ReqManager.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Services.InterfacesServices
{
    public interface IControllerActionService
    {
        IEnumerable<ControllerAction> GetAll();
        IEnumerable<ControllerAction> filterByController(string controllerName);
        IEnumerable<ControllerAction> filterByAction(string actionName);
        ControllerAction Get(int? id);
        void refresh(IEnumerable controllerActionApplication);
        void saveChanges();
    }
}
