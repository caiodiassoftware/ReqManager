using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data
{
    public class StoreSeedData : DropCreateDatabaseIfModelChanges<StoreEntities>
    {
        protected override void Seed(StoreEntities context)
        {
            //GetCategories().ForEach(c => context.Categories.Add(c));
            //GetGadgets().ForEach(g => context.Gadgets.Add(g));

            //context.Commit();
        }
    }
}
