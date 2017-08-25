using Gym5DAL;
using System.Collections.Generic;

namespace Gym5BLL.Implementations
{
    public interface IPersonaBLL
    {
        bool Agregar(PersonaGym persona);
        bool Modificar(PersonaGym persona);
        bool Eliminar(string id);
        List<PersonaGym> BuscarTodos(string empresa);
        PersonaGym BuscarPorId(string idInstructor);
    }
}