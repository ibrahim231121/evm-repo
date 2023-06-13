using Crossbones.ALPR.Models.CapturedPlate;
using Crossbones.Modules.Common.Exceptions;

namespace Crossbones.ALPR.Common.Validation
{
    public static class CapturedPlateValidations
    {
        public static void ValidateCapturedPlateItem(CapturedPlateItem capturedPlateItem)
        {
            if (capturedPlateItem == null)
            {
                throw new InvalidValue("CapturedPlate Item is invalid");
            }

            if (string.IsNullOrEmpty(capturedPlateItem.NumberPlate))
            {
                throw new InvalidValue("Captured plate number can not be empty");
            }

            if (capturedPlateItem.CapturedAt == DateTime.MinValue || capturedPlateItem.CapturedAt == DateTime.MaxValue)
            {
                throw new InvalidValue("Invalid CapturedAt date time.");
            }

            if (capturedPlateItem.Confidence < 0 || capturedPlateItem.Confidence > 100)
            {
                throw new InvalidValue("Invalid CapturedPlate confidence value.");
            }

            if (capturedPlateItem.TicketNumber < 0)
            {
                throw new InvalidValue("Invalid CapturedPlate TicketNumber value.");
            }

            if (capturedPlateItem.Latitude < -90 || capturedPlateItem.Latitude > 90)
            {
                throw new InvalidValue("Invalid CapturedPlate Latitude value.");
            }

            if (capturedPlateItem.Longitude < -180 || capturedPlateItem.Longitude > 180)
            {
                throw new InvalidValue("Invalid CapturedPlate Longitude value.");
            }
        }

        public static void ValidateCapturePlateSummaryItem(CapturePlatesSummaryItem capturePlateSummaryItem)
        {
            if (capturePlateSummaryItem == null)
            {
                throw new InvalidValue("CapturedPlate Item is invalid");
            }

            if (string.IsNullOrEmpty(capturePlateSummaryItem.UnitId))
            {
                throw new InvalidValue("CapturePlateSummary UnitId can not be empty");
            }

            if (string.IsNullOrEmpty(capturePlateSummaryItem.LoginId))
            {
                throw new InvalidValue("CapturePlateSummary LoginId can not be empty");
            }

            if (capturePlateSummaryItem.StationId < 0)
            {
                throw new InvalidValue("Invalid CapturePlatesSummary StationId value.");
            }

            if (capturePlateSummaryItem.ClientId < 0)
            {
                throw new InvalidValue("Invalid CapturePlatesSummary ClientId value.");
            }

            if (capturePlateSummaryItem.CaptureDate == DateTime.MinValue || capturePlateSummaryItem.CaptureDate == DateTime.MaxValue)
            {
                throw new InvalidValue("Invalid CapturePlatesSummary CaptureDate date time.");
            }
        }

        public static void ValidateCapturePlateSummaryStatusItem(CapturePlatesSummaryStatusItem capturePlateSummaryStatusItem)
        {
            if (capturePlateSummaryStatusItem == null)
            {
                throw new InvalidValue("CapturePlateSummaryStatus Item is invalid");
            }

            if (capturePlateSummaryStatusItem.LastExecutionDate == DateTime.MinValue || capturePlateSummaryStatusItem.LastExecutionDate == DateTime.MaxValue)
            {
                throw new InvalidValue("Invalid CapturePlateSummaryStatus LastExecutionDate date time.");
            }

            if (capturePlateSummaryStatusItem.LastExecutionEndDate == DateTime.MinValue || capturePlateSummaryStatusItem.LastExecutionEndDate == DateTime.MaxValue)
            {
                throw new InvalidValue("Invalid CapturePlateSummaryStatus LastExecutionDate date time.");
            }

            if (capturePlateSummaryStatusItem.LastExecutionDate > capturePlateSummaryStatusItem.LastExecutionEndDate)
            {
                throw new InvalidValue("CapturePlateSummaryStatus LastExecutionDate date time can not be greater than LastExecutionEndDate");
            }

        }
    }
}
