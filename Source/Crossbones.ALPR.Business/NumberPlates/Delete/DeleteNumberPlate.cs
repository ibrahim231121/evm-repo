using Corssbones.ALPR.Business;
using Crossbones.ALPR.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossbones.ALPR.Business.NumberPlates.Delete
{
    public class DeleteNumberPlate : SysSerialItemMessage
    {
        public DeleteNumberPlate(SysSerial id) : base(id)
        {
            
        }
    }
}
