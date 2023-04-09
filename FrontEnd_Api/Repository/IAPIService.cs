using FrontEnd_Api.Models;
using System.Collections.Generic;

namespace FrontEnd_Api.Repository
{
    public interface IAPIService
    {
        Task<List<ModClieClientes>> MostrarCliente();
        Task<List<ModClieClientes>> BuscarCliente(string codigo);
        Task<bool> RegistrarCliente(ModClieClientes modClie);
        Task<bool> ActualizarCliente(ModClieClientes modClie);
        Task<bool> EliminarCliente(string codigo);

    }
}