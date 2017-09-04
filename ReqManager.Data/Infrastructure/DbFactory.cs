using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private ReqManagerEntities dbContext;

        public ReqManagerEntities Init()
        {
            return dbContext ?? (dbContext = new ReqManagerEntities());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
