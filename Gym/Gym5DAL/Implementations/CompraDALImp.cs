using Gym5DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym5DAL.Implementations
{
    public class CompraDALImp : GenericRepository<Compra>, ICompraDAL
    {
        public CompraDALImp(GymContext context) : base(context)
        {
        }

        public GymContext GymContext
        {
            get { return Context as GymContext; }
        }
    }
}
