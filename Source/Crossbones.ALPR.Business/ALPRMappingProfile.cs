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
            CreateMap<E.HotListNumberPlate, HotListNumberPlateItem>().ReverseMap();
            CreateMap<E.Hotlist, HotListItem>().ReverseMap();

            CreateMap<E.ALPRExportDetail, ExportDetailItem>().ReverseMap();
            CreateMap<E.HotlistDataSource, HotListDataSourceItem>().ReverseMap();
            CreateMap<E.SourceType, SourceTypeItem>().ReverseMap();

            CreateMap<E.Hotlist, AddHotListItem>().ReverseMap();
            CreateMap<E.HotlistDataSource, AddHotListDataSourceItem>().ReverseMap();

            CreateMap<HotListNumberPlate, HotListNumberPlateItem>().ReverseMap();

            CreateMap<Hotlist, HotListItem>().ReverseMap();

            CreateMap<NumberPlate, NumberPlateItem>()
                .ForMember(dest => dest.DateOfInterest, x => x.MapFrom(src => src.DateOfInterest.ToString("yyyy-MM-dd HH:mm")))
                .ForMember(dest => dest.StateName, x => x.MapFrom(src => src.State.StateName));
            CreateMap<AddNumberPlate, NumberPlate>();

            CreateMap<NumberPlateTemp, NumberPlateTempItem>().ReverseMap();
            CreateMap<AddNumberPlatesTemp, NumberPlateTemp>().ReverseMap();

            CreateMap<State, StateItem>().ReverseMap();
        }
    }
}