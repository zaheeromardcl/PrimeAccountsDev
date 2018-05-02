using PrimeActs.Data.Mapping;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Data.Contexts
{
    public class SetupContext : DataContextBase
    {
        static SetupContext()
        {
            Database.SetInitializer<PAndIContext>(null);
        }

        public SetupContext()
            : base("Name=PAndIContext")
        { }

        public DbSet<SetupGlobal> SetupGlobals { get; set; }
        public DbSet<SetupLocal> SetupLocals { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new SetupGlobalMap());
            modelBuilder.Configurations.Add(new SetupLocalMap());
        }
    }
}
