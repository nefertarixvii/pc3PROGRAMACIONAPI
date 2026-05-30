using ApiInteligenteTareas.DTOs;
using ApiInteligenteTareas.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiInteligenteTareas.Controllers
{
    [ApiController]
    [Route("api/tareas-externas")]
    public class TareasExternasController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public TareasExternasController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoExternoDto>>> GetTodos()
        {
            try
            {
                var response = await _httpClient.GetAsync(
                    "https://jsonplaceholder.typicode.com/todos");

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode(
                        503,
                        "La API externa no está disponible.");
                }

                var todos =
                    await response.Content.ReadFromJsonAsync<List<TodoApiResponse>>();

                var resultado = todos!.Select(t => new TodoExternoDto
                {
                    ExternalId = t.Id,
                    Titulo = t.Title,
                    Completado = t.Completed
                });

                return Ok(resultado);
            }
            catch
            {
                return StatusCode(
                    503,
                    "Error al comunicarse con la API externa.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoExternoDto>> GetTodo(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync(
                    $"https://jsonplaceholder.typicode.com/todos/{id}");

                if (response.StatusCode ==
                    System.Net.HttpStatusCode.NotFound)
                {
                    return NotFound();
                }

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode(
                        503,
                        "La API externa no está disponible.");
                }

                var todo =
                    await response.Content.ReadFromJsonAsync<TodoApiResponse>();

                if (todo == null)
                {
                    return NotFound();
                }

                return Ok(new TodoExternoDto
                {
                    ExternalId = todo.Id,
                    Titulo = todo.Title,
                    Completado = todo.Completed
                });
            }
            catch
            {
                return StatusCode(
                    503,
                    "Error al comunicarse con la API externa.");
            }
        }
    }
}