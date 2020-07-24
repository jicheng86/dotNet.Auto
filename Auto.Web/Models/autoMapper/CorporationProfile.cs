using AutoMapper;

using Auto.Model.Dto;
using Auto.Model.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Auto.Model.autoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Corporation, CorporationDto>();
            CreateMap<CorporationDto, Corporation>()
                .ForMember(des => des.AreaID, opt => opt.MapFrom(src => src.AreaID.LastOrDefault()))
                .ForMember(des => des.CreationTime, opt => opt.MapFrom(src => DateTime.Now.ToLocalTime()))
                .ForMember(des => des.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(des => des.IsDeleted, opt => opt.MapFrom(src => false));
            CreateMap<CorporationDtoCreation, Corporation>()
                .ForMember(des => des.AreaID, opt => opt.MapFrom(src => src.AreaID.LastOrDefault()))
                .ForMember(des => des.CreationTime, opt => opt.MapFrom(src => DateTime.Now.ToLocalTime()))
                .ForMember(des => des.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(des => des.IsDeleted, opt => opt.MapFrom(src => false));
            //CreateMap<Corporation, CorporationDto>()
            //    .ForMember(destinationMember: des => des.CorporationAddress, memberOptions: opt => opt.MapFrom(src => src.CorporationAddress));//属性字段单独映射
        }

    }
}
