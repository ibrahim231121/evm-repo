using Crossbones.ALPR.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corssbones.ALPR.Business.CapturedPlate
{
    public class CapturedPlateMessage: MessageBase
    {
        public SysSerial Id { get; }
        public CapturedPlateMessage(SysSerial _id) => Id = _id;
    }
}
