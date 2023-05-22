using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Corssbones.ALPR.Database.Entities;

public partial class NumberPlate
{
    [Key]
    [Column("sysSerial")]
    public long SysSerial { get; set; }

    [Column("ImportSerialID")]
    [StringLength(50)]
    [Unicode(false)]
    public string? ImportSerialId { get; set; }

    [Column("NCICNumber")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Ncicnumber { get; set; }

    [Column("AgencyID")]
    [StringLength(50)]
    [Unicode(false)]
    public string? AgencyId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateOfInterest { get; set; }

    [Column("NumberPlate")]
    [StringLength(50)]
    [Unicode(false)]
    public string? NumberPlate1 { get; set; }

    [Column("StateID")]
    [StringLength(50)]
    [Unicode(false)]
    public string? StateId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? LicenseYear { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? LicenseType { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? VehicleYear { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? VehicleMake { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? VehicleModel { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? VehicleStyle { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? VehicleColor { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Note { get; set; }

    public short? InsertType { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LastUpdatedOn { get; set; }

    public byte[] LastTimeStamp { get; set; } = null!;

    public short Status { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? FirstName { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? LastName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Alias { get; set; }

    [Unicode(false)]
    public string? ViolationInfo { get; set; }

    [Unicode(false)]
    public string? Notes { get; set; }
}
