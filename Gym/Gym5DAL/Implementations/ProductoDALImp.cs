using Gym5DAL.Interfaces;


namespace Gym5DAL.Implementations
{
    public class ProductoDALImp : GenericRepository<Producto>, IProductoDAL
    {
        public ProductoDALImp(GymContext context) : base(context)
        {
        }

        public GymContext GymContext
        {
            get { return Context as GymContext; }
        }
    }
}
