using Crossbones.ALPR.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corssbones.ALPR.Business.SourceType.Delete
{
    public class DeleteSourceType : SysSerialItemMessage
    {
        public DeleteSourceType(SysSerial id) : base(id)
        {

        }
    }
}
