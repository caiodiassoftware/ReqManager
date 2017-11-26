using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Requirements.Interfaces;
using ReqManager.Model;

namespace ReqManager.Data.Repositories.Requirements.Classes
{
    public class RequestStatusRepository : RepositoryBase<RequestStatus>, IRequestStatusRepository
    {
        public RequestStatusRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
