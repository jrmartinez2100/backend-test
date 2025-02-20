using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<MarcaAuto, MarcaAutoDto>();
            CreateMap<MarcaAutoDto, MarcaAuto>();
        }
    }
}
