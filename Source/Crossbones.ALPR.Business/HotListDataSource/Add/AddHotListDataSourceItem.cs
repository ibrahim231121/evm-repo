﻿using Corssbones.ALPR.Business;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.Items;

namespace Crossbones.ALPR.Business.HotListDataSource.Add
{
    public class AddHotListDataSourceItem : RecIdItemMessage
    {
        public AddHotListDataSourceItem(RecId id) : base(id)
        {
        }

        public HotListDataSourceDTO Item { get; set; }

    }
}
