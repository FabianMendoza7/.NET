using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/actores")]
    public class ActoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ActoresController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActorDTO>>> Get()
        {
            var entidades = await context.Actores.ToListAsync();
            return mapper.Map<List<ActorDTO>>(entidades);
        }

        [HttpGet("{id}", Name = "obtenerActor")]
        public async Task<ActionResult<ActorDTO>> Get(int id)
        {
            var entidad = await context.Actores.FirstOrDefaultAsync(x => x.Id == id);

            if (entidad == null)
            {
                return NotFound();
            }

            return mapper.Map<ActorDTO>(entidad);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ActorCreacionDTO actorCreacionDTO)
        {
            var entidad = mapper.Map<Actor>(actorCreacionDTO);
            context.Add(entidad);
            await context.SaveChangesAsync();
            var dto = mapper.Map<ActorDTO>(entidad);

            return new CreatedAtRouteResult("obtenerActor", new { id = entidad.Id }, dto);
        }
    }
}
