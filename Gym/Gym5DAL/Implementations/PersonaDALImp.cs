using Gym5DAL.Interfaces;

namespace Gym5DAL.Implementations
{
    public class PersonaDALImp : GenericRepository<PersonaGym>, IPersonaDAL
    {
        public PersonaDALImp(GymContext context) : base(context)
        {
        }

        public GymContext GymContext
        {
            get { return Context as GymContext; }
        }
    }
}
