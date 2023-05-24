using Crossbones.ALPR.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corssbones.ALPR.Business.NumberPlatesTemp
{
    public class NumberPlatesTempMessage : MessageBase
    {
        public SysSerial Id { get; set; }
        public NumberPlatesTempMessage(SysSerial _id)
        {
            Id = _id;
        }
    }
}
