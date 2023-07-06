
using Corssbones.ALPR.Business;
using Crossbones.ALPR.Common.ValueObjects;
using DTO = Crossbones.ALPR.Models.DTOs;

namespace Crossbones.ALPR.Business.HotList.Change
{
    public class ChangeHotListItem : RecIdItemMessage
    {

        public ChangeHotListItem(RecId recId, DTO.HotListDTO itemToUpdate) : base(recId)
        {
            ItemToUpdate = itemToUpdate;
        }

        public DTO.HotListDTO ItemToUpdate { get; }
    }
}
