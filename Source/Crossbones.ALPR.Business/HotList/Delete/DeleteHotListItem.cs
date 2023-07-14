using Corssbones.ALPR.Business;
using Crossbones.ALPR.Common.ValueObjects;

namespace Crossbones.ALPR.Business.HotList.Delete
{
    public class DeleteHotListItem : RecIdItemMessage
    {
        public DeleteHotListItem(RecId recId, List<long> idsToDelete = null) : base(recId)
        {
            IdsToDelete = idsToDelete;
        }

        public List<long> IdsToDelete { get; }
    }
}
