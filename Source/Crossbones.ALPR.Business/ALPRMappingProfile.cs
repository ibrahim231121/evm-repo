using AutoMapper;
using Corssbones.ALPR.Business.CapturedPlate.Add;
using Corssbones.ALPR.Business.NumberPlatesTemp.Add;
using Corssbones.ALPR.Business.SourceType.Add;
using Crossbones.ALPR.Business.HotListDataSource.Add;
using Crossbones.ALPR.Business.NumberPlates.Add;
using Crossbones.ALPR.Models.CapturedPlate;
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

            #endregion

            #region ALPRExportDetail

            CreateMap<Entities.AlprExportDetail, DTO.ExportDetailDTO>().ReverseMap();
            CreateMap<Entities.HotlistDataSource, DTO.HotListDataSourceDTO>().ReverseMap();
            CreateMap<Entities.SourceType, DTO.SourceTypeDTO>().ReverseMap();

            #endregion

            #region Hotlist

            CreateMap<DTO.HotListDTO,Entities.Hotlist>().ForMember(dest => dest.Source, x => x.NullSubstitute(null));
            CreateMap<Entities.Hotlist,DTO.HotListDTO>();

            #endregion

            #region HotlistDataSource

            CreateMap<DTO.HotListDataSourceDTO, Entities.HotlistDataSource>();

            #endregion

            #region NumberPlate

            CreateMap<Entities.NumberPlate, DTO.NumberPlateDTO>()
                .ForMember(dest => dest.DateOfInterest, x => x.MapFrom(src => src.DateOfInterest/*.ToString("yyyy-MM-dd HH:mm")*/))
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

            #region CapturePlatesSummary

            CreateMap<Entities.CapturePlatesSummary, CapturePlatesSummaryDTO>().ReverseMap();

            #endregion

            #region CapturePlatesSummaryStatus

            CreateMap<Entities.CapturePlatesSummaryStatus, CapturePlatesSummaryStatusDTO>().ReverseMap();

            #endregion

            #region SourceType

            CreateMap<AddSourceType, Entities.SourceType>();

            #endregion

            #region UserCapturedPlate

            CreateMap<AddUserCapturedPlateItem, Entities.UserCapturedPlate>();

            #endregion

            #region CapturePlate

            CreateMap<CapturedPlateDTO, Entities.CapturedPlate>().ReverseMap();

            #endregion

        }
    }
}