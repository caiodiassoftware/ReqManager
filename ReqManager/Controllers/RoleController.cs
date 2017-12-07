using ReqManager.Entities.Acess;
using ReqManager.ManagerController;
using ReqManager.Services.Acess.Interfaces;
using ReqManager.Services.Documents.Interfaces;
using System.Web;
using System.Web.Mvc;

namespace ReqManager.Controllers
{
    public class RoleController : BaseController<RoleEntity>
    {
        private IRequirementDocumentService reqDocument { get; set; }

        public RoleController(IRoleService service, IRequirementDocumentService reqDocument) : base(service)
        {
            this.reqDocument = reqDocument;
        }

        public override ActionResult Index()
        {

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.ContentType = "application/octet-stream";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.AddHeader("content-disposition", "attachment;filename= Test.pdf");
            Response.Buffer = true;
            Response.Clear();
            var bytes = reqDocument.printDocumentRequirementProject(1, 1);
            Response.OutputStream.Write(bytes, 0, bytes.Length);
            Response.OutputStream.Flush();


            return base.Index();
        }
    }
}
