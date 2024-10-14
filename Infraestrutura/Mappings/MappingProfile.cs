using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AutoMapper;
using SolutionTrack.Dominio.DTOs;
using SolutionTrack.Dominio.Entidades;
using SolutionTrack.Dominio.ModelViews;

namespace SolutionTrack.Infraestrutura.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UsuarioDTO, Usuario>();
            CreateMap<Usuario, UsuarioModelView>();
            CreateMap<UsuarioDTO, UsuarioModelView>();
        }
    }
}