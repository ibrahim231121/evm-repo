using Crossbones.Modules.Common.DbContextWithChannel;
using Crossbones.Transport.Pipes;
using Microsoft.EntityFrameworkCore;

namespace Corssbones.ALPR.Database.Entities;

public partial class AlprContext : DbContextWithChannel
{
    public AlprContext()
    {
    }


    public AlprContext(DbContextOptions options, IMessageChannel channel, string tenantServiceId) : base(options, channel, tenantServiceId)
    {
    }

    public virtual DbSet<ALPRExportDetail> ALPRExportDetail { get; set; }

    public virtual DbSet<CapturePlatesSummary> CapturePlatesSummaries { get; set; }

    public virtual DbSet<CapturePlatesSummaryStatus> CapturePlatesSummaryStatuses { get; set; }

    public virtual DbSet<CapturedPlate> CapturedPlates { get; set; }

    public virtual DbSet<HotListNumberPlate> HotListNumberPlates { get; set; }

    public virtual DbSet<Hotlist> Hotlists { get; set; }

    public virtual DbSet<HotlistDataSource> HotlistDataSources { get; set; }

    public virtual DbSet<NumberPlate> NumberPlates { get; set; }

    public virtual DbSet<NumberPlateTemp> NumberPlatesTemps { get; set; }

    public virtual DbSet<Sequence> Sequences { get; set; }

    public virtual DbSet<ServiceInfo> ServiceInfos { get; set; }

    public virtual DbSet<SourceType> SourceTypes { get; set; }

    public virtual DbSet<UserCapturedPlate> UserCapturedPlates { get; set; }

    public virtual DbSet<State> States { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=ALPR;Trusted_Connection=True;TrustServerCertificate=True;", x => x.UseNetTopologySuite());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_CI_AS");

        modelBuilder.Entity<ALPRExportDetail>(entity =>
        {
            entity.Property(e => e.ExportedOn).HasDefaultValueSql("(getutcdate())");
        });

        modelBuilder.Entity<CapturePlatesSummary>(entity =>
        {
            entity.HasKey(e => new { e.CapturePlateId, e.UserId }).HasName("PK_CapturePlatesSummary_Composite");
        });

        modelBuilder.Entity<CapturedPlate>(entity =>
        {
            entity.Property(e => e.CaptureType).HasDefaultValueSql("((1))");
            entity.Property(e => e.RowGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            entity.Property(e => e.StorageType).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<HotListNumberPlate>(entity =>
        {
            entity.HasKey(e => e.RecId);

            entity.Property(e => e.RecId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.HotListId).HasColumnName("HotListID");
            entity.Property(e => e.LastTimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.LastUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.NumberPlatesId).HasColumnName("NumberPlatesID");

            entity.HasOne(d => d.HotList).WithMany(p => p.HotListNumberPlates)
                .HasForeignKey(d => d.HotListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HotListNumberPlates_HotList");

            entity.HasOne(d => d.NumberPlate).WithMany(p => p.HotListNumberPlates)
                .HasForeignKey(d => d.NumberPlatesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HotListNumberPlates_NumberPlates");
        });

        modelBuilder.Entity<Hotlist>(entity =>
        {
            entity.HasKey(e => e.RecId).HasName("PK_HotList");

            entity.Property(e => e.AlertPriority).HasDefaultValueSql("((1))");
            entity.Property(e => e.LastTimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(d => d.Source).WithMany(p => p.Hotlists).HasConstraintName("FK_HotList_HotListDataSource");
        });

        modelBuilder.Entity<HotlistDataSource>(entity =>
        {
            entity.HasKey(e => e.RecId).HasName("PK_HotListSource");

            entity.HasOne(d => d.SourceType).WithMany(p => p.HotlistDataSources)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HotListSource_SourceType");
        });

        modelBuilder.Entity<NumberPlate>(entity =>
        {
            entity.HasKey(e => e.RecId);

            entity.Property(e => e.RecId).ValueGeneratedNever();
            entity.Property(e => e.AgencyId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AgencyID");
            entity.Property(e => e.Alias)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DateOfInterest).HasColumnType("datetime");
            entity.Property(e => e.FirstName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ImportSerialId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ImportSerialID");
            entity.Property(e => e.InsertType).HasDefaultValueSql("((2))");
            entity.Property(e => e.LastName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.LastTimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.LastUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.LicenseType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LicenseYear)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ncicnumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NCICNumber");
            entity.Property(e => e.Note)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Notes).IsUnicode(false);
            entity.Property(e => e.LicensePlate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NumberPlate");
            entity.Property(e => e.StateId).HasColumnName("StateID");
            entity.Property(e => e.Status).HasDefaultValueSql("((0))");
            entity.Property(e => e.VehicleColor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.VehicleMake)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.VehicleModel)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.VehicleStyle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.VehicleYear)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ViolationInfo).IsUnicode(false);

            entity.HasOne(d => d.State).WithMany(p => p.NumberPlates)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK_NumberPlates_State");
        });

        modelBuilder.Entity<NumberPlateTemp>(entity =>
        {
            entity.HasKey(e => e.RecId);

            entity.ToTable("NumberPlatesTemp");

            entity.Property(e => e.RecId).ValueGeneratedNever();
            entity.Property(e => e.AgencyId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AgencyID");
            entity.Property(e => e.Alias)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DateOfInterest).HasColumnType("datetime");
            entity.Property(e => e.FirstName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ImportSerialId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ImportSerialID");
            entity.Property(e => e.InsertType).HasDefaultValueSql("((2))");
            entity.Property(e => e.LastName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.LastTimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.LastUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.LicenseType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LicenseYear)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NCICNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NCICNumber");
            entity.Property(e => e.Note)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.NumberPlate)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StateId).HasColumnName("StateID");
            entity.Property(e => e.Status).HasDefaultValueSql("((0))");
            entity.Property(e => e.VehicleColor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.VehicleMake)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.VehicleModel)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.VehicleStyle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.VehicleYear)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ViolationInfo).IsUnicode(false);

            entity.HasOne(d => d.State).WithMany(p => p.NumberPlatesTemps)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK_NumberPlatesTemp_State");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.ToTable("State");

            entity.Property(e => e.RecId)
                .ValueGeneratedOnAdd()
                .HasColumnName("RecId");
            entity.Property(e => e.StateName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
