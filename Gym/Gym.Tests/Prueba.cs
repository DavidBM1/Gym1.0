using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gym5BLL.Implementations;
using Gym5DAL;

namespace Gym.Tests
{
    [TestClass]
    class Prueba
    {
        [TestMethod]
        public void Agregar()
        {
            LogEmpresaBLLImp log = new LogEmpresaBLLImp();
            LogEmpresa logg = new LogEmpresa();
            logg.idNombreEmpresa = "Michael";
            logg.pass = "1234";
            log.Agregar(logg);
        }
    }
}
