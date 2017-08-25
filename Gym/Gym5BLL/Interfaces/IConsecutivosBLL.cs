using Gym5DAL;

namespace Gym5BLL.Implementations
{
    public interface IConsecutivosBLL
    {
        bool Agregar(Consecutivo consecutivo);
        bool Modificar(Consecutivo consecutivo);
        bool Eliminar(Consecutivo consecutivo);
        Consecutivo BuscarPorId(string idConsecutivo);
    }
}
