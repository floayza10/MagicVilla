using FrontEnd_Api.Models;
using FrontEnd_Api.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FrontEnd_Api.Controllers
{
    public class HomeController : Controller
    {

        private readonly IAPIService _aPIService;

        public HomeController(IAPIService aPIService)
        {
            _aPIService = aPIService;
        }

        public async Task<IActionResult>  Index()
        {
            List<ModClieClientes> modClieClientes = await _aPIService.MostrarCliente();
            return View(modClieClientes);
        }

        [HttpGet]
        public async Task<IActionResult> Regresar()
        {
            List<ModClieClientes> modClieClientes = await _aPIService.MostrarCliente();
            return View("_ListaClientes",modClieClientes);

        }

   
        public async Task<IActionResult> BuscarCliente( int codigo)
        {
            //ModClieClientes modClieClientes = await _aPIService.BuscarCliente(Convert.ToString(codigo));
            List<ModClieClientes> modClieClientes = await _aPIService.BuscarCliente(Convert.ToString(codigo));

            
            return View("ActualizarCliente",modClieClientes);
        }

        [HttpGet]
        public IActionResult NuevoCliente()
        {
            return View("RegistrarCliente");
 
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarCliente(ModClieClientes modClie)
        {
            bool respuesta = await _aPIService.RegistrarCliente(modClie);
            string[] msj = new string[2];
            if (respuesta)
            {
                msj[0] = "1";
                msj[1] = "Cliente registrado con exito";

            }
            else
            {
                msj[0] = "0";
                msj[1] = "Error inesperado al intentar registrar el nuevo cliente";
            }
            return Json(msj);
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarCliente(ModClieClientes modClie)
        {
            bool respuesta = await _aPIService.ActualizarCliente(modClie);
            string[] msj = new string[2];
            if (respuesta)
            {
                msj[0] = "1";
                msj[1] = "Cliente actualizado con exito";

            }
            else
            {
                msj[0] = "0";
                msj[1] = "Error inesperado al intentar Actualiza el nuevo cliente";
            }
            return Json(msj);

        }

        [HttpDelete]
        public async Task<IActionResult> EliminarCliente(string codigoCliente)
        {
            string[] msj = new string[2];
            bool respuesta = await _aPIService.EliminarCliente(codigoCliente);
            
            if (respuesta)
            {
                msj[0] = "1";
                msj[1] = "Cliente Eliminado con exito";

            }
            else
            {
                msj[0] = "0";
                msj[1] = "Error inesperado al intentar Eliminado el nuevo cliente";
            }
            return Json(msj);

        }


    }
}