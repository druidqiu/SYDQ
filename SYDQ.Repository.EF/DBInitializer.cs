using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYDQ.Repository.EF
{
    public class DBInitializer
    {
        public static void SetInitializer()
        {
            Database.SetInitializer<EntitiesContext>(new DataInitializer());
            //Database.SetInitializer<EntitiesContext>(null);
        }
    }
}
