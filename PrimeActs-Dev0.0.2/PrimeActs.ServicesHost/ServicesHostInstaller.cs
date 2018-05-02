using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace PrimeActs.ServicesHost
{
    [RunInstaller(true)]
    public sealed class ServicesHostInstaller : ServiceInstaller
    {
        public ServicesHostInstaller()
        {
            this.Description = "PrimeActs Windows Servie to host On-Premises services";
            this.DisplayName = "PrimeActs Windows Service";
            this.ServiceName = "PrimeActsServicesHost";
            this.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
        }
    }

    [RunInstaller(true)]
    public sealed class ServicesHostProcessInstaller : ServiceProcessInstaller
    {
        public ServicesHostProcessInstaller()
        {
            this.Account = ServiceAccount.NetworkService;
        }
    }
}
