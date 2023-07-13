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

            CreateMap<HotListNumberPlate, DTO.HotListNumberPlateDTO>().ReverseMap();

            CreateMap<Hotlist, DTO.HotListDTO>().ReverseMap();

            CreateMap<NumberPlate, DTO.NumberPlateDTO>()
                .ForMember(dest => dest.DateOfInterest, x => x.MapFrom(src => src.DateOfInterest.ToString("yyyy-MM-dd HH:mm")))
                .ForMember(dest => dest.StateName, x => x.MapFrom(src => src.State.StateName));
            CreateMap<AddNumberPlate, NumberPlate>();

            CreateMap<NumberPlateTemp, DTO.NumberPlateTempDTO>().ReverseMap();
            CreateMap<AddNumberPlatesTemp, NumberPlateTemp>().ReverseMap();

            CreateMap<State, DTO.StateDTO>().ReverseMap();
        }
    }
}