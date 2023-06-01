using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossbones.ALPR.Models.Items
{
    public class HotListDataSourceItem
    {
        public string? Name { get; set; }
        public string? SourceName { get; set; }
        public virtual SourceTypeItem SourceType { get; set; }

    }
}
