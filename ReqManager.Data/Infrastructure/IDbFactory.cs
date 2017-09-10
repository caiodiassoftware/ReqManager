using ReqManager.Data.DataAcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        ReqManagerEntities Init();
    }
}
