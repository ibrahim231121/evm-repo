using Corssbones.ALPR.Business;
using Crossbones.ALPR.Common.ValueObjects;
using Corssbones.ALPR.Database.Entities;

namespace Crossbones.ALPR.Business.HotListDataSource.Add
{
    public class AddHotListDataSourceItem : SysSerialItemMessage
    {
        public AddHotListDataSourceItem(SysSerial id) : base(id)
        {
        }
        public string Name { get; set; }
        public string SourceName { get; set; }
        public long? AgencyID { get; set; }
        public long SourceTypeID { get; set; }
        public decimal? SchedulePeriod { get; set; }
        public DateTime? LastUpdated { get; set; }
        public bool? IsExpire { get; set; }
        public string SchemaDefinition { get; set; }
        public long? LastUpdateExternalHotListID { get; set; }
        public string ConnectionType { get; set; }
        public string Userid { get; set; }
        public string locationPath { get; set; }
        public string Password { get; set; }
        public int? port { get; set; }
        public DateTime? LastRun { get; set; }
        public short Status { get; set; }
        public bool SkipFirstLine { get; set; }
        public string StatusDesc { get; set; }
        public SourceType SourceType { get; set; } = null!;
    }
}
