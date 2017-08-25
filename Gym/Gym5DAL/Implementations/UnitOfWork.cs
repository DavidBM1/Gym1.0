using System;
using Gym5DAL.Interfaces;
using Gym5DAL;
using Gym5DAL.Implementations;

namespace Gym5DAL.Implementations
{
    public class UnitOfWork : IDisposable
    {
        private readonly GymContext context;
        public ICierreDAL cierreDAL;
        public ICierreProductoDAL cierreProductoDAL;
        public IConsecutivosDAL consecutivosDAL;
        public IInstructorDAL instructorDAL;
        public ILogEmpresaDAL logEmpresaDAL;
        public ILogUsuarioDAL logUsuarioDAL;
        public IPersonaDAL personaDAL;
        public IPersonaMedidasDAL personaMedidasDAL;
        public IProductoDAL productoDAL;
        public ICompraDAL compraDAL;
        public ICompraProductoDAL compraProductoDAL;

        public UnitOfWork(GymContext _context)
        {
            context = _context;
            cierreDAL = new CierreDALImp(context);
            cierreProductoDAL = new CierreProductoDALImp(context);
            consecutivosDAL = new ConsecutivosDALImp(context);
            instructorDAL = new InstructorDALImp(context);
            logEmpresaDAL = new LogEmpresaDALImp(context);
            logUsuarioDAL = new LogUsuarioDALImp(context);
            personaDAL = new PersonaDALImp(context);
            personaMedidasDAL = new PersonaMedidasDALImp(context);
            productoDAL = new ProductoDALImp(context);
            compraDAL = new CompraDALImp(context);
            compraProductoDAL = new CompraProductoDALImp(context);
        }

        public void Complete()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

    }
}
