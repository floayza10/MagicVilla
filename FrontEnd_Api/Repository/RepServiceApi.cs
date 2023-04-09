using FrontEnd_Api.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace FrontEnd_Api.Repository
{
    public class RepServiceApi : IAPIService
    {
        private static string _usuarioApi = "";
        private static string _passwordApi = "";
        private static string _urlBaseApi = "";
        private static string _tokenApi = "";

        private readonly HttpClient _httpClient;

        public RepServiceApi()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json").Build();
            _usuarioApi = builder.GetSection("ConexionApi:UsuarioApi").Value;
            _passwordApi = builder.GetSection("ConexionApi:PasswordApi").Value;
            _urlBaseApi = builder.GetSection("ConexionApi:UrlBase").Value;

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_urlBaseApi);
        }



        public async Task LoginApi()
        {
            ModLoginApi modLogin = new();
            modLogin.Usuario = _usuarioApi;
            modLogin.Pass = _passwordApi;
            StringContent content = new(JsonConvert.SerializeObject(modLogin), Encoding.UTF8, "Application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(_urlBaseApi + "token", content);
            //HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:7093/api/token", content);

            _tokenApi = await response.Content.ReadAsStringAsync();
        }

        public async Task<List<ModClieClientes>> MostrarCliente()
        {
        
            List<ModClieClientes>? modClieClientes = new();
            await LoginApi();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenApi);
            HttpResponseMessage response = await _httpClient.GetAsync("clientes/MostrarCliente");
            //HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:7093/api/clientes/MostrarCliente");
        
            if (response.IsSuccessStatusCode)
            {
                string respuesta = await response.Content.ReadAsStringAsync();
                modClieClientes = JsonConvert.DeserializeObject<List<ModClieClientes>>(respuesta);
                return modClieClientes;
            }
            else
            {
                return await Task.FromResult(modClieClientes);   
            }
        }

        public async Task<List<ModClieClientes>> BuscarCliente(string codigo)
        {
            List<ModClieClientes>? modClieClientes = new();
            await LoginApi();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenApi);
            HttpResponseMessage response = await _httpClient.GetAsync("clientes/BuscarCliente/" + codigo);
            if (response.IsSuccessStatusCode)
            {
                string respuesta = await response.Content.ReadAsStringAsync();
                try
                {

                    modClieClientes = JsonConvert.DeserializeObject<List<ModClieClientes>>(respuesta);

                }
                catch (Exception)
                {

                    throw;
                }
               

  
                return modClieClientes;
            }
            else
            {
                return await Task.FromResult(modClieClientes);
            }
        }

        public async Task<bool> RegistrarCliente(ModClieClientes modClieClientes)
        {
            await LoginApi();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenApi);
            StringContent content = new(JsonConvert.SerializeObject(modClieClientes), Encoding.UTF8, "Application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("clientes/RegistrarCliente/" , content);
            if (response.IsSuccessStatusCode)
            {
                 return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<bool> ActualizarCliente(ModClieClientes modClieClientes)
        {
            await LoginApi();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenApi);
            StringContent content = new(JsonConvert.SerializeObject(modClieClientes), Encoding.UTF8, "Application/json");
            HttpResponseMessage response = await _httpClient.PutAsync("clientes/ActualizarCliente/", content);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }

        }



        public async Task<bool> EliminarCliente(string codigo)
        {
            //await LoginApi();
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenApi);
            //HttpResponseMessage response = await _httpClient.DeleteAsync("clientes/EliminarCliente/" + codigo);
            HttpResponseMessage response = await _httpClient.DeleteAsync("http://localhost:7093/api/clientes/EliminarCliente/" + codigo);
        
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
