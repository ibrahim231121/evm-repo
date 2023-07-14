using AutoMapper;
using Corssbones.ALPR.Business.NumberPlatesTemp.Add;
using Corssbones.ALPR.Database.Entities;
using Crossbones.ALPR.Business.HotList.Add;
using Crossbones.ALPR.Business.HotListDataSource.Add;
using Crossbones.ALPR.Business.NumberPlates.Add;
using DTO = Crossbones.ALPR.Models.DTOs;
using E = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business
{
    public class ALPRMappingProfile : Profile
    {
        public ALPRMappingProfile()
        {
            CreateMap<E.HotListNumberPlate, DTO.HotListNumberPlateDTO>().ReverseMap();
            CreateMap<E.Hotlist, DTO.HotListDTO>().ReverseMap();

            CreateMap<E.ALPRExportDetail, DTO.ExportDetailDTO>().ReverseMap();
            CreateMap<E.HotlistDataSource, DTO.HotListDataSourceDTO>().ReverseMap();
            CreateMap<E.SourceType, DTO.SourceTypeDTO>().ReverseMap();

            CreateMap<E.Hotlist, AddHotListItem>().ReverseMap();
            CreateMap<E.HotlistDataSource, AddHotListDataSourceItem>().ReverseMap();

            CreateMap<E.HotListNumberPlate, DTO.HotListNumberPlateDTO>().ReverseMap();

            CreateMap<E.Hotlist, DTO.HotListDTO>().ReverseMap();

            CreateMap<E.NumberPlate, DTO.NumberPlateDTO>()
                .ForMember(dest => dest.DateOfInterest, x => x.MapFrom(src => src.DateOfInterest.ToString("yyyy-MM-dd HH:mm")))
                .ForMember(dest => dest.StateName, x => x.MapFrom(src => src.State.StateName));

            CreateMap<DTO.NumberPlateDTO, NumberPlate>();
            CreateMap<DTO.HotListDataSourceMappingDTO, DTO.NumberPlateDTO>();

            CreateMap<AddNumberPlate, NumberPlate>();

            CreateMap<E.NumberPlateTemp, DTO.NumberPlateTempDTO>().ReverseMap();
            CreateMap<AddNumberPlatesTemp, E.NumberPlateTemp>().ReverseMap();

            CreateMap<E.State, DTO.StateDTO>().ReverseMap();
        }
    }
}