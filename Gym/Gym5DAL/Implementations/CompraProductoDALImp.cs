using Gym5DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym5DAL.Implementations
{
    public class CompraProductoDALImp : GenericRepository<CompraProducto>, ICompraProductoDAL
    {
        public CompraProductoDALImp(GymContext context) : base(context)
        {
        }

        public GymContext GymContext
        {
            get { return Context as GymContext; }
        }
    }
}
