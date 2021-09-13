
using Aplicacion.Cursos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private readonly IMediator _mediador;

        public CursosController(IMediator mediador)
        {
            this._mediador = mediador;
        }

        [HttpGet]
        public async Task<ActionResult<List<Curso>>> Get()
        {
            return await _mediador.Send(new Consulta.ListaCursos());
        }

        // http://localhost:5000/api/cursos/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> Detalle(int id)
        {
            return await _mediador.Send(new ConsultaId.CursoUnico { Id = id });
        }
    }
}