using Microsoft.ServiceBus;
using Ninject;
using PrimeActs.Data.Contexts;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.ServicesHost
{
    partial class ServicesHostService : ServiceBase
    {
        private readonly StandardKernel _kernel;
        private ServiceHost _serviceHost = null;

        public ServicesHostService()
        {
            InitializeComponent();

            _kernel = new StandardKernel();
            _kernel.Load(Assembly.GetExecutingAssembly());
        }

        protected override void OnStart(string[] args)
        {
            var setupLocalService = _kernel.Get<ISetupLocalService>();

            var settingServerName = setupLocalService.Find("PrintServiceServerName");
            var serverName = "localhost"; // settingServerName != null ? settingServerName.SetupValueNvarchar : "localhost";
            var settingServerPort = setupLocalService.Find("PrintServiceServerPort");
            var serverPort = settingServerPort != null ? settingServerPort.SetupValueInt : 49501;

            _serviceHost = new ServiceHost(typeof(PrintService));
            var binding = new NetTcpBinding
            {
                TransferMode = TransferMode.Buffered,
                MaxBufferPoolSize = 2147483647,
                MaxBufferSize = 2147483647,
                MaxReceivedMessageSize = 2147483647
            };
            _serviceHost.AddServiceEndpoint(
                typeof(IPrintService),
                binding, string.Format("net.tcp://{0}:{1}/print", serverName, serverPort));

            var settingRelayServiceName = setupLocalService.Find("RelayServiceName");
            var serviceNamespace = settingRelayServiceName != null ? settingRelayServiceName.SetupValueNvarchar : null;
            var settingRelayServiceSecret = setupLocalService.Find("RelayServiceSecret");
            var serviceSecret = settingRelayServiceSecret != null ? settingRelayServiceSecret.SetupValueNvarchar : null;

            if (!string.IsNullOrEmpty(serviceNamespace) && !string.IsNullOrEmpty(serviceSecret))
            {
                var relayBinding = new NetTcpRelayBinding
                {
                    TransferMode = TransferMode.Buffered,
                    MaxBufferPoolSize = 2147483647,
                    MaxBufferSize = 2147483647,
                    MaxReceivedMessageSize = 2147483647
                };
                _serviceHost.AddServiceEndpoint(
                    typeof(IPrintService),
                    relayBinding,
                    ServiceBusEnvironment
                        .CreateServiceUri("sb", serviceNamespace, "print"))
                        .Behaviors.Add(new TransportClientEndpointBehavior
                        {
                            TokenProvider = TokenProvider
                                .CreateSharedSecretTokenProvider(
                                    "owner",
                                    serviceSecret
                                )
                        });
            }
            _serviceHost.Open();
        }

        protected override void OnStop()
        {
            _serviceHost.Close();
        }
    }
}
