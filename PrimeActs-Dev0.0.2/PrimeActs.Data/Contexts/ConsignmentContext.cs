#region

using System.Data.Entity;
using PrimeActs.Data.Mapping;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.EntityFramework;

#endregion

namespace PrimeActs.Data.Contexts
{
    public class ConsignmentContext : DataContextBase
    {
        static ConsignmentContext()
        {
            Database.SetInitializer<ConsignmentContext>(null);
        }

        public ConsignmentContext()
            : base("Name=PAndIContext")
        {
        }

        public DbSet<Consignment> Consignments { get; set; }
        public DbSet<ConsignmentItem> ConsignmentItems { get; set; }
        public DbSet<ConsignmentItemArrival> ConsignmentItemArrivals { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Produce> Produces { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<PurchaseType> PurchaseTypeValues { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ConsignmentMap());
            modelBuilder.Configurations.Add(new ConsignmentItemMap());
            modelBuilder.Configurations.Add(new ConsignmentItemArrivalMap());
            modelBuilder.Configurations.Add(new DepartmentMap());
            modelBuilder.Configurations.Add(new ProduceMap());
            modelBuilder.Configurations.Add(new SupplierMap());
            modelBuilder.Configurations.Add(new CountryMap());
            modelBuilder.Configurations.Add(new PurchaseTypeMap());
        }
    }
}