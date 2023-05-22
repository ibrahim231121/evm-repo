namespace Crossbones.ALPR.Models
{
    public class RowCount
    {
        public RowCount(long total) => Total = total;
        public long Total { get; }
    }
}
