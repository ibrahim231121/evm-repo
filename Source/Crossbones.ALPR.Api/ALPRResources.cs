﻿using System.ComponentModel;

namespace Crossbones.ALPR.Api
{
    public enum ALPRResources
    {
        [Description("HotList")]
        HotList = 1,

        [Description("CapturedPlate")]
        CapturedPlate = 2,

        [Description("UserCapturedPlate")]
        UserCapturedPlate = 3,

        [Description("CapturePlateSummary")]
        CapturePlateSummary = 4,

        [Description("CapturePlateSummaryStatus")]
        CapturePlateSummaryStatus = 5,
        [Description("ExortDetail")]
        ExortDetail = 6,
        [Description("HotListNumberPlate")]
        HotListNumberPlate = 7,
        [Description("NumberPlate")]
        NumberPlate = 8,
        [Description("NumberPlatesTemp")]
        NumberPlatesTemp = 9,
    }
}
