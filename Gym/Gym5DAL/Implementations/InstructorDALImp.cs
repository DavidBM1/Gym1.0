using Gym5DAL.Interfaces;

namespace Gym5DAL.Implementations
{
    public class InstructorDALImp : GenericRepository<InstructorGym>, IInstructorDAL
    {
        public InstructorDALImp(GymContext context) : base(context)
        {
        }

        public GymContext GymContext
        {
            get { return Context as GymContext; }
        }
    }
}
