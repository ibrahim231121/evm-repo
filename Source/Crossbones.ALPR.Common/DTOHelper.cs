using Corssbones.ALPR.Database.Entities;
using Crossbones.ALPR.Models.CapturedPlate;

namespace Crossbones.ALPR.Common
{
    public static class DTOHelper
    {
        #region ConvertToDTO

        public static CapturePlatesSummaryDTO ConvertToDTO(CapturePlatesSummary item)
        {
            if (item == null)
            {
                return null;
            }
            var capturePlatesSummaryItem = new CapturePlatesSummaryDTO()
            {
                UserId = item.UserId,
                CapturePlateId = item.CapturePlateId,
                CaptureDate = item.CaptureDate,
                ClientId = item.ClientId,
                HasAlert = item.HasAlert,
                HasTicket = item.HasTicket,
                LoginId = item.LoginId,
                StationId = item.StationId,
                UnitId = item.UnitId
            };
            return capturePlatesSummaryItem;
        }

        public static CapturePlatesSummaryStatusDTO ConvertToDTO(CapturePlatesSummaryStatus item)
        {
            if (item == null)
            {
                return null;
            }
            var capturePlatesSummaryItem = new CapturePlatesSummaryStatusDTO()
            {
                SyncId = item.SyncId,
                LastExecutionDate = item.LastExecutionDate,
                LastExecutionEndDate = item.LastExecutionEndDate,
                StatusDesc = item.StatusDesc,
                StatusId = item.StatusId
            };
            return capturePlatesSummaryItem;
        }

        #endregion

        #region ConvertFromDTO

        public static CapturePlatesSummary ConvertFromDTO(CapturePlatesSummaryDTO item)
        {
            if (item == null)
            {
                return null;
            }
            var capturePlatesSummaryItem = new CapturePlatesSummary()
            {
                UserId = item.UserId,
                CapturePlateId = item.CapturePlateId,
                CaptureDate = item.CaptureDate,
                ClientId = item.ClientId,
                HasAlert = item.HasAlert,
                HasTicket = item.HasTicket,
                LoginId = item.LoginId,
                StationId = item.StationId,
                UnitId = item.UnitId
            };
            return capturePlatesSummaryItem;
        }

        public static CapturePlatesSummaryStatus ConvertFromDTO(CapturePlatesSummaryStatusDTO item)
        {
            if (item == null)
            {
                return null;
            }
            var capturePlatesSummaryItem = new CapturePlatesSummaryStatus()
            {
                SyncId = item.SyncId,
                LastExecutionDate = item.LastExecutionDate,
                LastExecutionEndDate = item.LastExecutionEndDate,
                StatusDesc = item.StatusDesc,
                StatusId = item.StatusId
            };
            return capturePlatesSummaryItem;
        }

        #endregion
    }
}
