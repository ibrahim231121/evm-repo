using AutoMapper;
using Corssbones.ALPR.Business.NumberPlatesTemp.Add;
using Crossbones.ALPR.Business.HotList.Add;
using Crossbones.ALPR.Business.HotListDataSource.Add;
using Crossbones.ALPR.Business.NumberPlates.Add;
using DTO = Crossbones.ALPR.Models.DTOs;
using Entities = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business
{
    public class ALPRMappingProfile : Profile
    {
        public ALPRMappingProfile()
        {
            #region HotListNumberPlate

            CreateMap<Entities.HotListNumberPlate, DTO.HotListNumberPlateDTO>().ReverseMap();
            CreateMap<Entities.HotListNumberPlate, DTO.HotListNumberPlateDTO>().ReverseMap();

            #endregion

            #region ALPRExportDetail

            CreateMap<Entities.ALPRExportDetail, DTO.ExportDetailDTO>().ReverseMap();
            CreateMap<Entities.HotlistDataSource, DTO.HotListDataSourceDTO>().ReverseMap();
            CreateMap<Entities.SourceType, DTO.SourceTypeDTO>().ReverseMap();

            #endregion

            #region Hotlist Mapping

            CreateMap<Entities.Hotlist, AddHotListItem>().ReverseMap();
            CreateMap<Entities.Hotlist, DTO.HotListDTO>().ReverseMap();

            #endregion

            #region HotlistDataSource

            CreateMap<Entities.HotlistDataSource, AddHotListDataSourceItem>().ReverseMap();

            #endregion

            #region NumberPlate

            CreateMap<Entities.NumberPlate, DTO.NumberPlateDTO>()
                .ForMember(dest => dest.DateOfInterest, x => x.MapFrom(src => src.DateOfInterest.ToString("yyyy-MM-dd HH:mm")))
                .ForMember(dest => dest.StateName, x => x.MapFrom(src => src.State.StateName));
            CreateMap<AddNumberPlate, Entities.NumberPlate>();
            CreateMap<DTO.NumberPlateDTO, Entities.NumberPlate>();

            #endregion

            #region NumberPlateTemp

            CreateMap<Entities.NumberPlateTemp, DTO.NumberPlateTempDTO>().ReverseMap();
            CreateMap<AddNumberPlatesTemp, Entities.NumberPlateTemp>().ReverseMap();

            #endregion

            #region State

            CreateMap<Entities.State, DTO.StateDTO>().ReverseMap();

            #endregion
        }
    }
}