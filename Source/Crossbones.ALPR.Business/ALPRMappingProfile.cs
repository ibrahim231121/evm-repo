using Corssbones.ALPR.Database.Entities;
using AutoMapper;
using Crossbones.ALPR.Models.Items;

namespace Corssbones.ALPR.Business
{
    public class ALPRMappingProfile : Profile
    {
        public ALPRMappingProfile()
        {
            CreateMap<HotListNumberPlate, HotListNumberPlateItem>().ReverseMap();
            CreateMap<Hotlist, HotListItem>().ReverseMap();
            CreateMap<NumberPlate, NumberPlates>().ReverseMap();
            CreateMap<ALPRExportDetail, ExportDetailItem>().ReverseMap();
        }
    }
}