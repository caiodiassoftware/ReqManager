using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Link.Interfaces;
using ReqManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Data.Repositories.Link.Classes
{
    public class TypeLinkRepository : RepositoryBase<TypeLink>, ITypeLinkRepository
    {
        public TypeLinkRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
