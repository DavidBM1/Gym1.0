using Gym5DAL.Interfaces;

namespace Gym5DAL.Implementations
{
    public class ConsecutivosDALImp : GenericRepository<Consecutivo>, IConsecutivosDAL
    {
        public ConsecutivosDALImp(GymContext context) : base(context)
        {
        }

        public GymContext GymContext
        {
            get { return Context as GymContext; }
        }
    }
}
