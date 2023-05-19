using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Corssbones.ALPR.Database.Entities;

public partial class UserCapturedPlate
{
    [Key]
    [Column("sysSerial")]
    public long SysSerial { get; set; }

    [Column("UserID")]
    public long UserId { get; set; }

    [Column("CapturedID")]
    public long CapturedId { get; set; }
}
