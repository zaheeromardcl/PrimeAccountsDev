using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;
using Microsoft.Synchronization;


namespace PrimeSQLSync
{
    public partial class Service1 : ServiceBase
    {
        Timer SyncTimer;
        EventLog SyncEventLog;
        public Service1()
        {
            InitializeComponent();
            SyncTimer = new Timer();
            SyncEventLog = new EventLog();
            //Create a source in event log 
            if (!EventLog.SourceExists("PrimeSQLSyncC"))
            {
                EventLog.CreateEventSource("PrimeSQLSyncC", "");
        }
            SyncEventLog.Source = "PrimeSQLSync";
            SyncEventLog.Log = "";
            SyncEventLog.WriteEntry("WorkerRole1 entry point called");
        }

        protected override void OnStart(string[] args)
        {
            //Setup();
            SyncEventLog.WriteEntry("Setup run");
            SyncTimer.Interval = 120000; //2 minutes
            SyncTimer.Elapsed += new ElapsedEventHandler(this.OnSyncTimer);
            SyncTimer.Start();
            SyncEventLog.WriteEntry("Sync timer started");
        }

        protected override void OnStop()
        {
            SyncTimer.Stop();
        }

        public void OnSyncTimer(object sender, ElapsedEventArgs args)
        {
            SyncTimer.Stop();
            Sync();
            SyncEventLog.WriteEntry("End Sync");

            //Now restart timer so sync runs 2 minutes after last sync
            SyncTimer.Start();
        }
        private void Setup()
        {
            try
            {
                using (SqlConnection sqlMemberAzureConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AzureContext"].ConnectionString))
                {
                    using (SqlConnection sqlHubAzureConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LocalContext"].ConnectionString))
                    {
                        sqlHubAzureConn.Open();
                        DataTable schema = sqlHubAzureConn.GetSchema("Tables");
                        DbSyncScopeDescription myScope = new DbSyncScopeDescription("PrimeSync");
                        DbSyncTableDescription[] syncTables = new DbSyncTableDescription[schema.Rows.Count];
                        foreach (DataRow row in schema.Rows)
                        {
                            syncTables[syncTables.Length] = SqlSyncDescriptionBuilder.GetDescriptionForTable((string)row[2], sqlHubAzureConn);
                            // Add the table from above to the scope
                            myScope.Tables.Add(syncTables[syncTables.Length - 1]);

                            //DbSyncTableDescription Product = SqlSyncDescriptionBuilder.GetDescriptionForTable("SalesLT.Product", sqlHubAzureConn);
                            //myScope.Tables.Add(Product);
                        }
                        // Setup for the Hub Database
                        SqlSyncScopeProvisioning sqlServerProv = new SqlSyncScopeProvisioning(sqlHubAzureConn, myScope);
                        if (!sqlServerProv.ScopeExists("PrimeSync"))
                            // Apply the scope provisioning.
                            sqlServerProv.Apply();
                        // Setup For the Member Database
                        SqlSyncScopeProvisioning sqlAzureProv = new SqlSyncScopeProvisioning(sqlMemberAzureConn, myScope);
                        if (!sqlAzureProv.ScopeExists("PrimeSync"))
                            // Apply the scope provisioning.
                            sqlAzureProv.Apply();
                    }
                }
            }
            catch (Exception e)
            {
                SyncEventLog.WriteEntry("Sync Error: " + e.Message);
            }
        }
        private void Sync()
        {
            try
            {
                using (SqlConnection sqlMemberAzureConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AzureContext"].ConnectionString))
                {
                    using (SqlConnection sqlHubAzureConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LocalContext"].ConnectionString))
                    {
                        SyncOrchestrator syncOrchestrator = new SyncOrchestrator
                        {
                            LocalProvider = new SqlSyncProvider("PrimeSync", sqlHubAzureConn),
                            RemoteProvider = new SqlSyncProvider("PrimeSync", sqlMemberAzureConn),
                            Direction = SyncDirectionOrder.UploadAndDownload
                        };

                        syncOrchestrator.Synchronize();
                    }
                }
            }
            catch (Exception e)
            {
                SyncEventLog.WriteEntry("Sync Error: " + e.Message);
            }
        }
    }
}
