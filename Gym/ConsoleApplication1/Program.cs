using Gym5BLL.Implementations;
using Gym5DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            LogEmpresaBLLImp log = new LogEmpresaBLLImp();
            LogEmpresa logg = new LogEmpresa();
            logg.idNombreEmpresa = "Michael";
            logg.pass = "1234";
            log.Agregar(logg);
        }
    }
}
