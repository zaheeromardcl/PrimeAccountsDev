#region

using System.Data.Entity;
using PrimeActs.Data.Mapping;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.EntityFramework;

#endregion

namespace PrimeActs.Data.Contexts
{
    public class TicketContext : DataContextBase
    {
        static TicketContext()
        {
            Database.SetInitializer<PAndIContext>(null);
        }

        public TicketContext()
            : base("Name=PAndIContext")
        {
        }


        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketItem> TicketItems { get; set; }
        public DbSet<TicketRange> TicketRanges { get; set; }
        public DbSet<TransferType> TransferTypes { get; set; }
        public DbSet<TransactionTaxCode> TransactionTaxCodeCodes { get; set; }
        public DbSet<TransactionTaxRate> TransactionTaxRates { get; set; }
        public DbSet<Version> Versions { get; set; }
        public DbSet<WarehouseLocation> WarehouseLocations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TicketMap());
            modelBuilder.Configurations.Add(new TicketItemMap());
            modelBuilder.Configurations.Add(new TicketRangeMap());
            modelBuilder.Configurations.Add(new TransferTypeMap());
            modelBuilder.Configurations.Add(new TransactionTaxCodeMap());
            modelBuilder.Configurations.Add(new TransactionTaxRateMap());
            modelBuilder.Configurations.Add(new VersionMap());
            modelBuilder.Configurations.Add(new WarehouseLocationMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}