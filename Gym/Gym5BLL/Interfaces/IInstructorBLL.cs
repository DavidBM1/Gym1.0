using Gym5DAL;
using System.Collections.Generic;

namespace Gym5BLL.Implementations
{
    public interface IInstructorBLL
    {
        bool Agregar(InstructorGym instructor);
        bool Modificar(InstructorGym instructor);
        bool Eliminar(string id);
        List<InstructorGym> BuscarTodos(string empresa);
        InstructorGym BuscarPorId(string idInstructor);
    }
}
