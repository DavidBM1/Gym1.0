using Gym5DAL.Interfaces;

namespace Gym5DAL.Implementations
{
    public class PersonaMedidasDALImp : GenericRepository<PersonaMedidasGym>, IPersonaMedidasDAL
    {
        public PersonaMedidasDALImp(GymContext context) : base(context)
        {
        }

        public GymContext GymContext
        {
            get { return Context as GymContext; }
        }
    }
}
