using ReqManager.Data.Infrastructure;
using ReqManager.Data.InterfacesRepositories;
using ReqManager.Data.Repositories.Link.Interfaces;
using ReqManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Data.Repositories.Acess.Classes
{
    public class AttributeRepository : RepositoryBase<Attribute>, IAttributeRepository
    {
        public AttributeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
