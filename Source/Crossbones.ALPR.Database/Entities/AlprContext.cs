using System;
using System.Collections.Generic;
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

    public virtual DbSet<AlprexportDetail> AlprexportDetails { get; set; }

    public virtual DbSet<CapturePlatesSummary> CapturePlatesSummaries { get; set; }

    public virtual DbSet<CapturePlatesSummaryStatus> CapturePlatesSummaryStatuses { get; set; }

    public virtual DbSet<CapturedPlate> CapturedPlates { get; set; }

    public virtual DbSet<HotListNumberPlate> HotListNumberPlates { get; set; }

    public virtual DbSet<Hotlist> Hotlists { get; set; }

    public virtual DbSet<HotlistDataSource> HotlistDataSources { get; set; }

    public virtual DbSet<NumberPlate> NumberPlates { get; set; }

    public virtual DbSet<NumberPlatesTemp> NumberPlatesTemps { get; set; }

    public virtual DbSet<Sequence> Sequences { get; set; }

    public virtual DbSet<ServiceInfo> ServiceInfos { get; set; }

    public virtual DbSet<SourceType> SourceTypes { get; set; }

    public virtual DbSet<UserCapturedPlate> UserCapturedPlates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=ALPR;Trusted_Connection=True;TrustServerCertificate=True;", x => x.UseNetTopologySuite());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_CI_AS");

        modelBuilder.Entity<AlprexportDetail>(entity =>
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
            entity.Property(e => e.LastTimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken();
        });

        modelBuilder.Entity<Hotlist>(entity =>
        {
            entity.HasKey(e => e.SysSerial).HasName("PK_HotList");

            entity.Property(e => e.AlertPriority).HasDefaultValueSql("((1))");
            entity.Property(e => e.LastTimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(d => d.Source).WithMany(p => p.Hotlists).HasConstraintName("FK_HotList_HotListDataSource");
        });

        modelBuilder.Entity<HotlistDataSource>(entity =>
        {
            entity.HasKey(e => e.SysSerial).HasName("PK_HotListSource");

            entity.HasOne(d => d.SourceType).WithMany(p => p.HotlistDataSources)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HotListSource_SourceType");
        });

        modelBuilder.Entity<NumberPlate>(entity =>
        {
            entity.Property(e => e.InsertType).HasDefaultValueSql("((2))");
            entity.Property(e => e.LastTimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken();
        });

        modelBuilder.Entity<NumberPlatesTemp>(entity =>
        {
            entity.Property(e => e.InsertType).HasDefaultValueSql("((2))");
            entity.Property(e => e.LastTimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
