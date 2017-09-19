using ReqManager.Data.Infrastructure;
using ReqManager.Data.InterfacesRepositories;
using ReqManager.Data.Repositories.Link.Interfaces;
using ReqManager.Entities.Link;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Link.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Services.Link.Classes
{
    public class AttributesTypeLinkService : ServiceBase<AttributesTypeLink, AttributesTypeLinkEntity>, IAttributesTypeLinkService
    {
        public AttributesTypeLinkService(IAttributesTypeLinkRepository repository, IUnitOfWork unit) : base(repository, unit)
        {

        }
    }
}
