using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Corssbones.ALPR.Database.Entities;

public partial class HotListNumberPlate
{
    [Key]
    [Column("sysSerial")]
    public long SysSerial { get; set; }

    [Column("HotListID")]
    public int HotListId { get; set; }

    [Column("NumberPlatesID")]
    public long NumberPlatesId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LastUpdatedOn { get; set; }

    public byte[] LastTimeStamp { get; set; } = null!;
}
