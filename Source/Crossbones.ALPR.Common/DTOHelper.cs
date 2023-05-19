using Corssbones.ALPR.Database.Entities;
using Crossbones.ALPR.Models.CapturedPlate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Crossbones.ALPR.Common
{
    public static class DTOHelper
    {
        #region ConvertToDTO

        public static CapturePlatesSummaryItem ConvertToDTO(CapturePlatesSummary item)
        {
            if (item == null)
            {
                return null;
            }
            var capturePlatesSummaryItem = new CapturePlatesSummaryItem()
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

        public static CapturePlatesSummaryStatusItem ConvertToDTO(CapturePlatesSummaryStatus item)
        {
            if (item == null)
            {
                return null;
            }
            var capturePlatesSummaryItem = new CapturePlatesSummaryStatusItem()
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

        public static CapturePlatesSummary ConvertFromDTO(CapturePlatesSummaryItem item)
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

        public static CapturePlatesSummaryStatus ConvertFromDTO(CapturePlatesSummaryStatusItem item)
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
