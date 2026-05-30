using ApiInteligenteTareas.DTOs;
using ApiInteligenteTareas.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiInteligenteTareas.Controllers
{
    [ApiController]
    [Route("api/ml")]
    public class MlController : ControllerBase
    {
        private readonly SentimientoService _service;

        public MlController(
            SentimientoService service)
        {
            _service = service;
        }

        [HttpPost("sentimiento")]
        public ActionResult<SentimientoResponseDto>
            Analizar(
                SentimientoRequestDto request)
        {
            var resultado =
                _service.Analizar(
                    request.Comentario);

            return Ok(
                new SentimientoResponseDto
                {
                    Comentario =
                        request.Comentario,
                    Sentimiento =
                        resultado
                });
        }
    }
}