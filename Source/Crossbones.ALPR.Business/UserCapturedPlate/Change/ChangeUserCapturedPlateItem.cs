﻿using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.CapturedPlate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corssbones.ALPR.Business.CapturedPlate.Change
{
    public class ChangeUserCapturedPlateItem: SysSerialItemMessage
    {
        public ChangeUserCapturedPlateItem(SysSerial id, long userId, long capturedId):base(id)
        {
            UserId = userId;
            CapturedId = capturedId;
        }

        public long UserId { get; }
        public long CapturedId { get; }
    }
}
