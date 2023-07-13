﻿using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.CapturedPlate;

namespace Corssbones.ALPR.Business.CapturedPlate.Change
{
    public class ChangeCapturedPlateItem : RecIdItemMessage
    {
        public ChangeCapturedPlateItem(RecId id, CapturedPlateDTO updateItem) : base(id)
        {
            UpdateItem = updateItem;
        }

        public CapturedPlateDTO UpdateItem { get; }
    }
}
