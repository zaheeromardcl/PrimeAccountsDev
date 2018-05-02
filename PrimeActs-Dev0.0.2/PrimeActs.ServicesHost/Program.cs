using Microsoft.ServiceBus;
using PrimeActs.Data.Service;
using PrimeActs.Domain.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.ServicesHost
{
    class Program
    {
        private static bool HasCommand(string[] args, string command)
        {
            if (args == null || args.Length == 0 || string.IsNullOrEmpty(command))
                return false;

            return args.Any(a => string.Equals(a, command, StringComparison.OrdinalIgnoreCase));
        }

        static void Main(string[] args)
        {
            var servicesToRun = new ServiceBase[]
            {
                new ServicesHostService()
            };

            if (Environment.UserInteractive)
            {
                // Always run as console if in debug mode
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    RunInteractiveServices(servicesToRun);
                }
                else
                {
                    try
                    {
                        if (HasCommand(args, "-i") || HasCommand(args, "-install"))
                        {
                            Install(false, args);
                        }
                        else if (HasCommand(args, "-u") || HasCommand(args, "-uninstall"))
                        {
                            Install(true, args);
                        }
                        else
                        {
                            RunInteractiveServices(servicesToRun);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error : {0}", ex.GetBaseException().Message);
                    }
                }
            }
            else
            {
                // Run as service
                ServiceBase.Run(servicesToRun);
            }
        }

        private static void RunInteractiveServices(ServiceBase[] servicesToRun)
        {
            Console.WriteLine();
            Console.WriteLine("Start the services in interactive mode.");
            Console.WriteLine();

            // Get the method to invoke on each service to start it
            MethodInfo onStartMethod = typeof(ServiceBase).GetMethod("OnStart", BindingFlags.Instance | BindingFlags.NonPublic);
            
            // Start services loop
            foreach (ServiceBase service in servicesToRun)
            {
                Console.Write("Starting {0} ... ", service.ServiceName);
                onStartMethod.Invoke(service, new object[] { new string[] { } });
                Console.WriteLine("Started");
            }

            // Waiting the end
            Console.WriteLine();
            Console.WriteLine("Press any key to stop services...");
            Console.ReadKey();
            Console.WriteLine();

            // Get the method to invoke on each service to stop it
            MethodInfo onStopMethod = typeof(ServiceBase).GetMethod("OnStop", BindingFlags.Instance | BindingFlags.NonPublic);
            
            // Stop loop
            foreach (ServiceBase service in servicesToRun)
            {
                Console.Write("Stopping {0} ... ", service.ServiceName);
                onStopMethod.Invoke(service, null);
                Console.WriteLine("Stopped");
            }

            Console.WriteLine();
            Console.WriteLine("All services are stopped.");
        }

        internal static void Install(bool undo, string[] args)
        {
            try
            {
                Console.WriteLine(undo ? "uninstalling" : "installing");

                using (var installer = new AssemblyInstaller(typeof(Program).Assembly, args))
                {
                    IDictionary state = new Hashtable();
                    installer.UseNewContext = true;
                    try
                    {
                        if (undo)
                        {
                            installer.Uninstall(state);
                        }
                        else
                        {
                            installer.Install(state);
                            installer.Commit(state);
                        }
                    }
                    catch
                    {
                        try
                        {
                            installer.Rollback(state);
                        }
                        catch
                        {
                            // Swallow the rollback exception
                            // and let the real cause bubble up.
                        }

                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }
    }
}
