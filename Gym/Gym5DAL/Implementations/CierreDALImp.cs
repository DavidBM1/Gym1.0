using Gym5DAL.Interfaces;

namespace Gym5DAL.Implementations
{
    public class CierreDALImp : GenericRepository<Cierre>, ICierreDAL
    {
        public CierreDALImp(GymContext context) : base(context)
        {
        }

        public GymContext GymContext
        {
            get { return Context as GymContext; }
        }
    }
}
