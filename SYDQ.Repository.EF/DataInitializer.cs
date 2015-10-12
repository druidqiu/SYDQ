using System.Data.Entity;

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
        }
    }
}
