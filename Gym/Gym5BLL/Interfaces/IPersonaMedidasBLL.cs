using Gym5DAL;
using System.Collections.Generic;

namespace Gym5BLL.Implementations
{
    public interface IPersonaMedidasBLL
    {
        bool Agregar(PersonaMedidasGym personaMedidas);
        bool Modificar(PersonaMedidasGym personaMedidas);
        bool Eliminar(string id);
        List<PersonaMedidasGym> BuscarTodos();
        List<PersonaMedidasGym> BuscarPorPersona(string id);
        PersonaMedidasGym BuscarPorId(string idpro);
        string getMeses(string id);
    }
}