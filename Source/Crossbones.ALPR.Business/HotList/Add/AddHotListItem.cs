using Corssbones.ALPR.Business;
using Crossbones.ALPR.Common.ValueObjects;
using DTO = Crossbones.ALPR.Models.DTOs;

namespace Crossbones.ALPR.Business.HotList.Add
{
    public class AddHotListItem : RecIdItemMessage
    {
        public AddHotListItem(RecId id, DTO.HotListDTO itemToAdd) : base(id)
        {
            this.ItemToAdd = itemToAdd;
        }

        public DTO.HotListDTO ItemToAdd { get; }
    }
}
