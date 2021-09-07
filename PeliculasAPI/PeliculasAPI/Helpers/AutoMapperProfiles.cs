using AutoMapper;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Genero, GeneroDTO>().ReverseMap();
            CreateMap<GeneroCreacionDTO, Genero>();

            CreateMap<Actor, ActorDTO>().ReverseMap();
            CreateMap<ActorCreacionDTO, Actor>().ForMember(X => X.Foto, options => options.Ignore());
            CreateMap<ActorPatchDTO, Actor>().ReverseMap();

            CreateMap<Pelicula, PeliculaDTO>().ReverseMap();
            CreateMap<PeliculaCreacionDTO, Pelicula>()
                .ForMember(X => X.Poster, options => options.Ignore())
                .ForMember(X => X.PeliculasGeneros, options => options.MapFrom(MapPeliculasGeneros))
                .ForMember(X => X.PeliculasActores, options => options.MapFrom(MapPeliculasActores));

            CreateMap<Pelicula, PeliculaDetallesDTO>()
                .ForMember(X => X.Generos, options => options.MapFrom(MapPeliculasGeneros));

            CreateMap<PeliculaPatchDTO, Pelicula>().ReverseMap();
        }

        private List<GeneroDTO> MapPeliculasGeneros(Pelicula pelicula, ActorPeliculaDetalleDTO peliculaDetallesDTO)
        {
            var resultado = new List<GeneroDTO>();
            if(pelicula.PeliculasGeneros == null)
            {
                foreach(var generoPelicula in pelicula.PeliculasGeneros)
                {
                    resultado.Add(new GeneroDTO()
                    {
                        Id = generoPelicula.GeneroId,
                        Nombre = generoPelicula.Genero.Nombre
                    });
                }
            }

            return resultado;
        }

        private List<PeliculasGeneros> MapPeliculasGeneros(PeliculaCreacionDTO peliculaCreacionDTO, Pelicula pelicula)
        {
            var resultado = new List<PeliculasGeneros>();

            if(peliculaCreacionDTO.GenerosIDs == null)
            {
                return resultado;
            }

            foreach(var id in peliculaCreacionDTO.GenerosIDs)
            {
                resultado.Add(new PeliculasGeneros()
                {
                    GeneroId = id
                });
            }

            return resultado;
        }

        private List<PeliculasActores> MapPeliculasActores(PeliculaCreacionDTO peliculaCreacionDTO, Pelicula pelicula)
        {
            var resultado = new List<PeliculasActores>();

            if (peliculaCreacionDTO.Actores == null)
            {
                return resultado;
            }

            foreach(var actor in peliculaCreacionDTO.Actores)
            {
                resultado.Add(new PeliculasActores()
                {
                    ActorId = actor.ActorId,
                    Personaje = actor.Personaje
                });
            }

            return resultado;
        }
    }
}
