﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Corssbones.ALPR.Database.Entities;

[Table("NumberPlates")]
public partial class NumberPlate
{
    public long SysSerial { get; set; }

    public string? ImportSerialId { get; set; }

    public string? Ncicnumber { get; set; }

    public string? AgencyId { get; set; }

    public DateTime DateOfInterest { get; set; }

    public string LicensePlate { get; set; } = null!;

    public byte? StateId { get; set; }

    public string? LicenseYear { get; set; }

    public string? LicenseType { get; set; }

    public string? VehicleYear { get; set; }

    public string? VehicleMake { get; set; }

    public string? VehicleModel { get; set; }

    public string? VehicleStyle { get; set; }

    public string? VehicleColor { get; set; }

    public string? Note { get; set; }

    public short? InsertType { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? LastUpdatedOn { get; set; }

    public byte[]? LastTimeStamp { get; set; }

    public short? Status { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Alias { get; set; }

    public string? ViolationInfo { get; set; }

    public string? Notes { get; set; }

    public virtual ICollection<HotListNumberPlate> HotListNumberPlates { get; set; } = new List<HotListNumberPlate>();

    public virtual State? State { get; set; }
}
