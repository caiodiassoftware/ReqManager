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
    public class LinkBetweenRequirementRepository : RepositoryBase<LinkBetweenRequirement>, ILinkBetweenRequirementRepository
    {
        public LinkBetweenRequirementRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
