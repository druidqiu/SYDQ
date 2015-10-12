using System.Data.Entity;

namespace SYDQ.Repository.EF
{
    public class DbInitializer
    {
        public static void SetInitializer()
        {
            Database.SetInitializer(new DataInitializer());
            //Database.SetInitializer<EntitiesContext>(null);
        }
    }
}
