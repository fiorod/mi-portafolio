using FixGo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
//using Windows.Media.Protection.PlayReady;
using static System.Runtime.InteropServices.JavaScript.JSType;
//using static Java.Security.DrbgParameters;

namespace FixGo.Services
{
    class ApiService
    {
        private readonly HttpClient _httpClient = new();
        #region Login
        //public async Task<bool> LoginAsync(string username, string password)
        //{
        //    await Task.Delay(500); // Simula llamada HTTP
        //    return username == "cliente" && password == "1234";
        //}

        public async Task<LoginResponse?> LoginUserAsync(LoginRequest data)
        {
            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                HttpResponseMessage respuestaHttp = new HttpResponseMessage();

                var jsonContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                using (HttpClient httpClient = new HttpClient())
                {
                    respuestaHttp = await httpClient.PostAsync("http://74.208.150.44/FixGoAPI/api/usuario/iniciarSesion", jsonContent);
                }

                if (respuestaHttp.IsSuccessStatusCode)
                {
                    var responseContent = await respuestaHttp.Content.ReadAsStringAsync();

                    LoginResponse? res = new LoginResponse();
                    res = JsonConvert.DeserializeObject<LoginResponse>(responseContent);
                    return res;
                }
                else
                {
                    var errorContent = await respuestaHttp.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine($"Error HTTP: {respuestaHttp.StatusCode}, contenido: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al llamar al API: {ex.Message}");
            }

            return null;
        }
        #endregion

        #region Peticion
        public async Task<PeticionResponse> SubmitRequestAsync(PeticionRequest request)
        {
            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                HttpResponseMessage respuestaHttp = new HttpResponseMessage();

                var jsonContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                using (HttpClient httpClient = new HttpClient())
                {
                    respuestaHttp = await httpClient.PostAsync("http://74.208.150.44/FixGoAPI/api/peticion/crear", jsonContent);
                }

                if (respuestaHttp.IsSuccessStatusCode)
                {
                    var responseContent = await respuestaHttp.Content.ReadAsStringAsync();

                    PeticionResponse res = new PeticionResponse();
                    res = JsonConvert.DeserializeObject<PeticionResponse>(responseContent);
                    return res;
                }
                else
                {
                    var errorContent = await respuestaHttp.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine($"Error HTTP: {respuestaHttp.StatusCode}, contenido: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al llamar al API: {ex.Message}");
            }

            return null;
        }

        public async Task<List<Subcategoria>> GetSubcategoriasPorCategoriaAsync(int idCategoria)
        {
            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using var client = new HttpClient(handler);
                var request = new
                {
                    IdCategoria = idCategoria
                };

                var response = await _httpClient.PostAsJsonAsync("http://74.208.150.44/FixGoAPI/api/subcategorias/obtenerPorCategoria", request);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var subResponse = System.Text.Json.JsonSerializer.Deserialize<SubcategoriaResponse>(result, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return subResponse?.listaSubCategorias ?? new List<Subcategoria>();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Fallo al obtener subcategorías: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error GetSubcategorias: {ex.Message}");
            }

            return new List<Subcategoria>();
        }

        #endregion

        #region MainMenu
        public async Task<List<Categoria>> GetCategoriasAsync()
        {
            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using var client = new HttpClient(handler);
                var response = await client.PostAsync("http://74.208.150.44/FixGoAPI/api/categoria/obtener", null);


                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var result = System.Text.Json.JsonSerializer.Deserialize<CategoriaApiResponse>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return result?.categorias ?? new();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Fallo al obtener categorías: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al obtener categorías: {ex.Message}");
            }

            return new();
        }

        #endregion

        public async Task<UpdateUserResponse> UpdateProfileAsync(UpdateUserRequest request)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("http://74.208.150.44/FixGoAPI/api/usuario/actualizar", content);
            if (!response.IsSuccessStatusCode)
            {
                return new UpdateUserResponse
                {
                    resultado = false,
                    mensaje = new List<string> { "Error al conectarse con el servidor" }
                };
            }

            var result = await response.Content.ReadFromJsonAsync<UpdateUserResponse>();
            return result ?? new UpdateUserResponse { resultado = false };
        }

        //public async Task<List<Ticket>> GetAssignedRequestsAsync()
        //{
        //    await Task.Delay(300);

        //    return new List<Ticket>
        //    {
        //        new Ticket { Servicio = "Electricidad", Subcategoria = "Reparación", Dia = "Martes", Hora = "2:00 PM" },
        //        new Ticket { Servicio = "Limpieza", Subcategoria = "Mantenimiento", Dia = "Jueves", Hora = "9:00 AM" }
        //    };
        //}

        //public async Task<List<Ticket>> GetCompletedServicesAsync()
        //{
        //    await Task.Delay(500); // Simula carga

        //    return new List<Ticket>
        //    {
        //        new Ticket { Servicio = "Plomería", Subcategoria = "Reparación", Dia = "Lunes", Hora = "10:00 AM", Estado = "Finalizado" },
        //        new Ticket { Servicio = "Electricidad", Subcategoria = "Instalación", Dia = "Miércoles", Hora = "1:00 PM", Estado = "Finalizado" }
        //    };
        //}

        public async Task<List<Ticket>> GetAllUnassignedRequestsAsync()
        {
            await Task.Delay(400); // Simula la carga desde el backend

            return new List<Ticket>
            {
                new Ticket { Servicio = "Jardinería", Subcategoria = "Mantenimiento", Dia = "Viernes", Hora = "3:00 PM" },
                new Ticket { Servicio = "Electricidad", Subcategoria = "Reparación", Dia = "Martes", Hora = "10:00 AM" }
            };
        }

        #region Usuarios
        public async Task<RegisterResponse?> RegisterUserAsync(RegisterRequest data)
        {
            try
            {
                HttpResponseMessage respuestaHttp = new HttpResponseMessage();

                var jsonContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                using (HttpClient httpClient = new HttpClient())
                {
                    respuestaHttp = await httpClient.PostAsync("http://74.208.150.44/FixGoAPI/api/usuario/crear", jsonContent);
                }

                if (respuestaHttp.IsSuccessStatusCode)
                    {
                    var responseContent = await respuestaHttp.Content.ReadAsStringAsync();

                    RegisterResponse? res = new RegisterResponse();
                    res = JsonConvert.DeserializeObject<RegisterResponse>(responseContent);
                    return res;
                }
                else
                {
                    var errorContent = await respuestaHttp.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine($"Error HTTP: {respuestaHttp.StatusCode}, contenido: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al llamar al API: {ex.Message}");
            }

            return null;
        }

        public async Task<List<Worker>> GetTrabajadoresAsync(int? idCategoria = null)
        {
            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using var client = new HttpClient(handler);

                string baseUrl = "http://74.208.150.44/FixGoAPI/api/trabajadores/obtener";
                string url = idCategoria.HasValue
                    ? $"{baseUrl}?IdCategoria={idCategoria.Value}"
                    : $"{baseUrl}?IdCategoria=";

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var trabajadores = System.Text.Json.JsonSerializer.Deserialize<List<Worker>>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return trabajadores ?? new List<Worker>();
                }
                else
                {
                    Debug.WriteLine($"Error HTTP: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en GetTrabajadoresAsync: {ex.Message}");
            }

            return new List<Worker>();
        }


        public async Task<List<FeedbackResponse>> GetFeedbackAsync(FeedbackRequest data)
        {
            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using var client = new HttpClient(handler);
                var url = "http://74.208.150.44/FixGoAPI/api/feedback/obtener";

                var json = System.Text.Json.JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStringAsync();
                    var feedback = System.Text.Json.JsonSerializer.Deserialize<List<FeedbackResponse>>(res, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return feedback ?? new List<FeedbackResponse>();
                }
                else
                {
                    Debug.WriteLine($"Error HTTP: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al llamar al API: {ex.Message}");
            }

            return new List<FeedbackResponse>();
        }

        public async Task<cambioContrasennaResponse?> ChangePassword(int idUsuario, string newPass, string oldPass)
        {
            // Construcción del JSON para el consumo del API
            var changePasswordRequest = new
            {
                IdUsuario = idUsuario,
                ClaveAnterior = oldPass,
                ClaveNueva = newPass
            };

            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using var client = new HttpClient(handler);
                var url = "http://74.208.150.44/FixGoAPI/api/usuario/cambiarContrasenia"; 

                var json = System.Text.Json.JsonSerializer.Serialize(changePasswordRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    cambioContrasennaResponse? res = new cambioContrasennaResponse();
                    res = JsonConvert.DeserializeObject<cambioContrasennaResponse>(result);
                    return res;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine($"Error HTTP: {response.StatusCode}, contenido: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al llamar al API: {ex.Message}");
            }

            return null;
        }
        #endregion

        #region Agregar Resenia
        public async Task<bool> CrearReseniaAsync(ReqCreateResenia request)
        {
            try
            {
                var jsonContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsync("http://74.208.150.44/FixGoAPI/api/feedback/crear", jsonContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<dynamic>(json);
                        var exito = result.exito;
                        if (exito = true)
                             return true;
                        else
                            return false;
                    }
                    else
                    {
                        Debug.WriteLine($"Error HTTP al crear reseña: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al enviar reseña: {ex.Message}");
            }

            return false;
        }
        #endregion

        #region Tickets
        public async Task<List<TicketDto>> ObtenerHistorialTicketsAsync(int idCliente)
        {
            var request = new
            {
                IdCliente = idCliente,
                IdTrabajador = (int?)null,
                Estado = (string)null
            };

            var response = await _httpClient.PostAsJsonAsync("http://74.208.150.44/FixGoAPI/api/tiquete/obtener", request);
            if (!response.IsSuccessStatusCode)
                return new List<TicketDto>();

            var json = await response.Content.ReadFromJsonAsync<JsonElement>();
            if (json.TryGetProperty("listaTiquetes", out var lista))
            {
                return System.Text.Json.JsonSerializer.Deserialize<List<TicketDto>>(lista.GetRawText()) ?? new();
            }

            return new List<TicketDto>();
        }

        public async Task<bool> EliminarTicketAsync(int idTiquete)
        {
            var request = new
            {
                idTiquete = idTiquete
            };

            var response = await _httpClient.PostAsJsonAsync("http://74.208.150.44/FixGoAPI/api/tiquete/delete", request);

            if (!response.IsSuccessStatusCode) return false;

            var json = await response.Content.ReadFromJsonAsync<JsonElement>();
            return json.TryGetProperty("resultado", out var result) && result.GetBoolean();
        }
        #endregion

        #region Peticiones
        public async Task<List<PeticionGeneralResponse?>> GetPeticionesGeneralesAsync(PeticionGeneralRequest request)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            var jsonContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            using (HttpClient httpClient = new HttpClient())
            {
                response = await httpClient.PostAsync("http://74.208.150.44/FixGoAPI/api/peticion/obtenerPorCategoria", jsonContent);
            }

            if (response.IsSuccessStatusCode)
            {
                var resultJson = await response.Content.ReadAsStringAsync();

                return System.Text.Json.JsonSerializer.Deserialize<List<PeticionGeneralResponse>>(resultJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                })!;
            }
            else
            {
                return new List<PeticionGeneralResponse?>();
            }
        }
        #endregion

        #region Citas
        public async Task<ConsultaListaPeticionResponse?> ObtenerPeticionPorIdAsync(ConsultaPeticionIdRequest request)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();

                var jsonContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                using (HttpClient httpClient = new HttpClient())
                {
                    response = await httpClient.PostAsync("http://74.208.150.44/FixGoAPI/api/peticion/obtenerPorId", jsonContent);
                }

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return System.Text.Json.JsonSerializer.Deserialize<ConsultaListaPeticionResponse>(result, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener petición: {ex.Message}");
            }
            return null;
        }

        public async Task<CrearCitaResponse?> CrearCitaAsync(CrearCitaRequest request)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();

                var jsonContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                using (HttpClient httpClient = new HttpClient())
                {
                    response = await httpClient.PostAsync("http://74.208.150.44/FixGoAPI/api/cita/insertar", jsonContent);
                }

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return System.Text.Json.JsonSerializer.Deserialize<CrearCitaResponse>(result, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear cita: {ex.Message}");
            }
            return null;
        }
        #endregion

        #region Trabajadores
        //    public async Task<List<TrabajadorConResenas>> GetTrabajadoresConResenasAsync()
        //    {
        //        await Task.Delay(500); // Simulamos una pequeña espera

        //        return new List<TrabajadorConResenas>
        //{
        //    new TrabajadorConResenas
        //    {
        //        NombreTrabajador = "Juan García",
        //        Empresa = "Electric S.A",
        //        Servicio = "Plomería",
        //        Telefono = "87563214",
        //        Resenas = new List<Resena>
        //        {
        //            new Resena
        //            {
        //                Usuario = "María Torres",
        //                Calificacion = 4.5,
        //                Comentario = "Excelente trabajador, muy amable, llegó temprano."
        //            },
        //            new Resena
        //            {
        //                Usuario = "Pedro Jiménez",
        //                Calificacion = 4.5,
        //                Comentario = "Tiene mucha variedad de herramientas."
        //            }
        //        }
        //    },
        //    new TrabajadorConResenas
        //    {
        //        NombreTrabajador = "Carlos Pérez",
        //        Empresa = "Multiservicios CR",
        //        Servicio = "Electricista",
        //        Telefono = "88889999",
        //        Resenas = new List<Resena>
        //        {
        //            new Resena
        //            {
        //                Usuario = "Laura Rojas",
        //                Calificacion = 4.0,
        //                Comentario = "Buen trabajo, pero llegó unos minutos tarde."
        //            }
        //        }
        //    }
        //};
        //    }
        #endregion



        // Método para obtener los clientes
        public async Task<List<FixGo.Models.Cliente>> GetClientesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("http://74.208.150.44/FixGoAPI/api/cliente/obtenerTodos");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var clientes = JsonConvert.DeserializeObject<List<FixGo.Models.Cliente>>(content);
                    return clientes ?? new List<FixGo.Models.Cliente>();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en GetClientesAsync: {ex.Message}");
            }

            return new List<FixGo.Models.Cliente>();
        }

        // Método para eliminar un trabajador
        public async Task<bool> EliminarTrabajadorAsync(int idTrabajador)
        {
            var response = await _httpClient.PostAsJsonAsync("http://74.208.150.44/FixGoAPI/api/trabajadores/eliminar", new { IdTrabajador = idTrabajador });
            return response.IsSuccessStatusCode;
        }

        // Método para eliminar un cliente
        public async Task<bool> EliminarClienteAsync(int idCliente)
        {
            var response = await _httpClient.PostAsJsonAsync("http://74.208.150.44/FixGoAPI/api/clientes/eliminar", new { IdCliente = idCliente });
            return response.IsSuccessStatusCode;
        }

        // Método para agregar un trabajador
        public async Task<bool> AgregarTrabajadorAsync(ReqAgregarTrabajador request)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://74.208.150.44/FixGoAPI/api/trabajadores/agregar", jsonContent);

            return response.IsSuccessStatusCode;
        }

        // Método para agregar un cliente
        public async Task<bool> AgregarClienteAsync(ReqAgregarCliente request)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://74.208.150.44/FixGoAPI/api/clientes/agregar", jsonContent);

            return response.IsSuccessStatusCode;
        }
    }
}