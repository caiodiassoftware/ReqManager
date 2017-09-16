using ReqManager.Data.Infrastructure;
using ReqManager.Data.InterfacesRepositories;
using ReqManager.Data.Repositories.Link.Interfaces;
using ReqManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Data.Repositories.Link.Classes
{
    public class AttributesTypeLinkRepository : RepositoryBase<AttributesTypeLink>, IAttributesTypeLinkRepository
    {
        public AttributesTypeLinkRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
