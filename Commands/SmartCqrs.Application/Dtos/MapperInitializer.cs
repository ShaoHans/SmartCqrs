using AutoMapper;
using SmartCqrs.Application.Commands;
using SmartCqrs.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SmartCqrs.Application.Dtos
{
    public class MapperInitializer
    {
        public static void Init()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, UserDto>();

                cfg.CreateMap<User, UpdateUserInfoDto>();
                //.ForMember(des => des.CarBrands, opt => opt.MapFrom(src => src.CarBrand.DeserializeObject<List<CarBrandDto>>()))
                //.ReverseMap()
                //.ForPath(des => des.CarBrand, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.CarBrands)));

                cfg.CreateMap<PublishCarCommand, Car>()
                //.ForMember(des => des.Color, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.Colors)))
                .ForMember(des => des.Image, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.Images)));
            });
        }
    }
}
