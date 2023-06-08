using AutoMapper;
using Crossbones.ALPR.Models.Items;
using Crossbones.ALPR.Business.HotList.Add;
using Crossbones.ALPR.Business.HotListDataSource.Add;
using E = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business
{
    public class ALPRMappingProfile : Profile
    {
        public ALPRMappingProfile()
        {
            CreateMap<E.HotListNumberPlate, HotListNumberPlateItem>().ReverseMap();
            CreateMap<E.Hotlist, HotListItem>().ReverseMap();
            CreateMap<E.NumberPlate, NumberPlates>().ReverseMap();
            CreateMap<E.ALPRExportDetail, ExportDetailItem>().ReverseMap();
            CreateMap<E.HotlistDataSource, HotListDataSourceItem>().ReverseMap();
            CreateMap<E.SourceType, SourceTypeItem>().ReverseMap();

            CreateMap<E.Hotlist, AddHotListItem>().ReverseMap();
            CreateMap<E.HotlistDataSource, AddHotListDataSourceItem>().ReverseMap();

        }
    }
}