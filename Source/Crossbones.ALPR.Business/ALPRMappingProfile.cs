using AutoMapper;
using Corssbones.ALPR.Business.NumberPlatesTemp.Add;
using Corssbones.ALPR.Database.Entities;
using Crossbones.ALPR.Business.HotList.Add;
using Crossbones.ALPR.Business.HotListDataSource.Add;
using Crossbones.ALPR.Business.NumberPlates.Add;
using Crossbones.ALPR.Models.Items;
using E = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business
{
    public class ALPRMappingProfile : Profile
    {
        public ALPRMappingProfile()
        {
            CreateMap<E.HotListNumberPlate, HotListNumberPlateDTO>().ReverseMap();
            CreateMap<E.Hotlist, HotListDTO>().ReverseMap();

            CreateMap<E.ALPRExportDetail, ExportDetailDTO>().ReverseMap();
            CreateMap<E.HotlistDataSource, HotListDataSourceDTO>().ReverseMap();
            CreateMap<E.SourceType, SourceTypeDTO>().ReverseMap();

            CreateMap<E.Hotlist, AddHotListItem>().ReverseMap();
            CreateMap<E.HotlistDataSource, AddHotListDataSourceItem>().ReverseMap();

            CreateMap<HotListNumberPlate, HotListNumberPlateDTO>().ReverseMap();

            CreateMap<Hotlist, HotListDTO>().ReverseMap();

            CreateMap<NumberPlate, NumberPlateDTO>()
                .ForMember(dest => dest.DateOfInterest, x => x.MapFrom(src => src.DateOfInterest.ToString("yyyy-MM-dd HH:mm")))
                .ForMember(dest => dest.StateName, x => x.MapFrom(src => src.State.StateName));
            CreateMap<AddNumberPlate, NumberPlate>();

            CreateMap<NumberPlateTemp, NumberPlateTempDTO>().ReverseMap();
            CreateMap<AddNumberPlatesTemp, NumberPlateTemp>().ReverseMap();

            CreateMap<State, StateDTO>().ReverseMap();
        }
    }
}