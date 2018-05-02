using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class ConsignmentItemPriceReturnMap : EntityTypeConfiguration<PrimeActs.Domain.ConsignmentItemPriceReturn>
    {
        public ConsignmentItemPriceReturnMap()
        {
            this.ToTable("tblConsignmentItemPriceReturn");
            this.Property(t => t.ConsignmentItemPriceReturnID).HasColumnName("ConsignmentItemPriceReturnID");
            this.Property(t => t.ConsignmentItemID).HasColumnName("ConsignmentItemID");
            this.Property(t => t.ReturnQuantity).HasColumnName("ReturnQuantity");
            this.Property(t => t.ReturnUnitPrice).HasColumnName("ReturnUnitPrice");
            this.Property(t => t.ReturnDate).HasColumnName("ReturnDate");
            this.Property(t => t.UpdatedByUserID).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedByUserID).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
        }
    }
}
