using Crossbones.ALPR.Common.ValueObjects;
using DTO = Crossbones.ALPR.Models.DTOs;

namespace Corssbones.ALPR.Business.ExportDetail.Add
{
    public class AddExportDetail : RecIdItemMessage
    {
        public DTO.ExportDetailDTO ItemToAdd { get; set; }
        public AddExportDetail(RecId recId) : base(recId)
        {
        }
    }
}