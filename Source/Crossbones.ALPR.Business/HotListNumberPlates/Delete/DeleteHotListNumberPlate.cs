﻿using Crossbones.ALPR.Common.ValueObjects;

namespace Corssbones.ALPR.Business.HotListNumberPlates.Delete
{
    public class DeleteHotListNumberPlate : RecIdItemMessage
    {
        public DeleteHotListNumberPlate(RecId id) : base(id)
        {
        }
    }
}