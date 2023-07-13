using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossbones.ALPR.Models.Items
{
    public class HotListDataSourceMappingDTO
    {
        public string LicensePlate { get; set; } = string.Empty;
        public string DateOfInterest { get; set; } = string.Empty;
        public string LicenseType { get; set; } = string.Empty;
        public string Agency { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Alias { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Style { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public string NCICNumber { get; set; } = string.Empty;
        public string ImportSerial { get; set; } = string.Empty;
        public string ViolationInfo { get; set; } = string.Empty;

    }
}
