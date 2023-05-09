using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ALPR.Database.Entities;

[Index("RowGuid", Name = "UQ__Captured__B975DD838D88CBD6", IsUnique = true)]
public partial class CapturedPlate
{
    [Key]
    [Column("sysSerial")]
    public long SysSerial { get; set; }

    public Guid RowGuid { get; set; }

    [Column("CapturedAT", TypeName = "datetime")]
    public DateTime CapturedAt { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string NumberPlate { get; set; } = null!;

    public int? BackColor { get; set; }

    public short CameraIndex { get; set; }

    public int? Color { get; set; }

    public int? Confidence { get; set; }

    public int? PlateType { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? PlateState { get; set; }

    [Column("Region_Bottom")]
    public int RegionBottom { get; set; }

    [Column("Region_Left")]
    public int RegionLeft { get; set; }

    [Column("Region_Right")]
    public int RegionRight { get; set; }

    [Column("Region_Top")]
    public int RegionTop { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? CharactersConfidence { get; set; }

    public int? AvgConfidence { get; set; }

    public int GeoLocationCode { get; set; }

    [Column("HitListJSON")]
    [Unicode(false)]
    public string? HitListJson { get; set; }

    [StringLength(1000)]
    [Unicode(false)]
    public string? Notes { get; set; }

    [Column("IRSAClientID")]
    public int IrsaclientId { get; set; }

    public long ClientSerial { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LastUpdated { get; set; }

    public int NotifyOverride { get; set; }

    [Column("Client_RowGuid")]
    public Guid? ClientRowGuid { get; set; }

    public short Notify { get; set; }

    [Column("HotlistCSV")]
    [StringLength(1000)]
    [Unicode(false)]
    public string? HotlistCsv { get; set; }

    public short State { get; set; }

    public short Status { get; set; }

    public short XmlExportStatus { get; set; }

    public long? TicketNumber { get; set; }

    [Column("LastFormRecordID")]
    public int? LastFormRecordId { get; set; }

    [Column("AppliedCapturePolicyID")]
    public int? AppliedCapturePolicyId { get; set; }

    [Column("AppliedNotificationPolicyID")]
    public int? AppliedNotificationPolicyId { get; set; }

    public short StorageType { get; set; }

    [StringLength(1024)]
    [Unicode(false)]
    public string? UriLocation { get; set; }

    public short CaptureType { get; set; }

    public short? NotifyType { get; set; }
}
