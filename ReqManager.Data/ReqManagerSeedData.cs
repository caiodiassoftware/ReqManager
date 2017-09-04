using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Data
{
    public class ReqManagerSeedData : DropCreateDatabaseIfModelChanges<ReqManagerEntities>
    {
        protected override void Seed(ReqManagerEntities context)
        {
            //GetCategories().ForEach(c => context.Categories.Add(c));
            //GetGadgets().ForEach(g => context.Gadgets.Add(g));

            //context.Commit();
        }
    }
}
