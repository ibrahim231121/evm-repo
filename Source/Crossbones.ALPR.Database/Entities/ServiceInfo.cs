using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Corssbones.ALPR.Database.Entities;

[Keyless]
[Table("ServiceInfo")]
public partial class ServiceInfo
{
    [Column("CMT_Tenant_RecId")]
    public long CmtTenantRecId { get; set; }

    [StringLength(128)]
    public string TenantIdentifier { get; set; } = null!;

    [StringLength(25)]
    public string? ServiceVersion { get; set; }

    [Column("DBVersion")]
    [StringLength(25)]
    public string? Dbversion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedOn { get; set; }

    [StringLength(128)]
    public string TenantName { get; set; } = null!;
}
