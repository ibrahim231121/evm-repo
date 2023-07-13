using Corssbones.ALPR.Business.HotListNumberPlates.Add;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Common.Pagination;
using DTO = Crossbones.ALPR.Models.DTOs;

namespace Crossbones.ALPR.Api.HotListNumberPlates
{
    public interface IHotListNumberPlateService
    {
        /// <summary>
        /// It is used to Get All Hotlist Number Plate data from database
        /// </summary>
        /// <returns>List of Hotlist Number Plate records</returns>
        Task<PagedResponse<DTO.HotListNumberPlateDTO>> GetAll(Pager paging);

        /// <summary>
        /// Add HotList Number Plate
        /// </summary>
        /// <param name="addHotListNumber"></param>
        /// <returns></returns>
        Task<RecId> Add(DTO.HotListNumberPlateDTO request);

        /// <summary>
        /// Get HotList Number Plate by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<DTO.HotListNumberPlateDTO> Get(RecId Id);

        /// <summary>
        /// Edit or Update HotList Number Plate
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task Change(RecId Id, DTO.HotListNumberPlateDTO request);

        /// <summary>
        /// Delete HotList Number Plate through Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task Delete(RecId Id);

        /// <summary>
        /// Delete every HotList Number Plates
        /// </summary>
        /// <returns></returns>
        Task DeleteAll();

        Task<AddHotListNumberPlate> GetAddCommand(DTO.HotListNumberPlateDTO request);

    }
}