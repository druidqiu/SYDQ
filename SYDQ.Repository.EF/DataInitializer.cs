using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYDQ.Repository.EF
{
    public class DataInitializer : DropCreateDatabaseIfModelChanges<EntitiesContext>
    {
        protected override void Seed(EntitiesContext context)
        {
            InitData(context);
            base.Seed(context);
        }

        private void InitData(EntitiesContext context)
        {
            var now = DateTime.Now;
            var today = now.Date;

            //context.Commit();
        }
    }
}
