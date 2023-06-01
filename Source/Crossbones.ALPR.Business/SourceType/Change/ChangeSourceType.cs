using Crossbones.ALPR.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corssbones.ALPR.Business.SourceType.Change
{
    public class ChangeSourceType : SysSerialItemMessage
    {

        public ChangeSourceType(SysSerial id) : base(id)
        {

        }
        public string SourceTypeName { get; set; }
        public string Description { get; set; }
    }
}
