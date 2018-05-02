using Ninject.Modules;
using PrimeActs.Data.Contexts;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.ServicesHost
{
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            
            //Bind<IDataContextAsync>().To<SetupContext>();
            Bind<IDataContextAsync>().To<PAndIContext>();
            Bind<IUnitOfWorkAsync>().To<UnitOfWork>();
            
            // Logging
            Bind<ILogger>().To<Log4NetLogger>();

            // Repository
            Bind<IRepositoryAsync<SetupLocal>>().To<Repository<SetupLocal>>();

            // Service
            Bind<ISetupLocalService>().To<SetupLocalService>();
        }
    }
}
