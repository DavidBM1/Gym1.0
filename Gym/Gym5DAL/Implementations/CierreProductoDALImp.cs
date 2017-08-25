using Gym5DAL.Interfaces;

namespace Gym5DAL.Implementations
{
    public class CierreProductoDALImp : GenericRepository<CierreProducto>, ICierreProductoDAL
    {
        public CierreProductoDALImp(GymContext context) : base(context)
        {
        }

        public GymContext GymContext
        {
            get { return Context as GymContext; }
        }
    }
}
