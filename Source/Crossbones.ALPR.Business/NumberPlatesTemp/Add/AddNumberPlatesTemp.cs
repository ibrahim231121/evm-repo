using Crossbones.ALPR.Common.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corssbones.ALPR.Business.NumberPlatesTemp.Add
{
    public class AddNumberPlatesTemp : SysSerialItemMessage
    {
        public AddNumberPlatesTemp(SysSerial id) : base(id)
        {
            
        }
        public string ImportSerialId { get; set; }
        public string Ncicnumber { get; set; }
        public string AgencyId { get; set; }
        public DateTime DateOfInterest { get; set; }
        public string NumberPlate { get; set; }
        public string StateId { get; set; }
        public string LicenseYear { get; set; }
        public string LicenseType { get; set; }
        public string VehicleYear { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleStyle { get; set; }
        public string VehicleColor { get; set; }
        public string Note { get; set; }
        public short InsertType { get; set; }
        public short Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Alias { get; set; }
        public string ViolationInfo { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
