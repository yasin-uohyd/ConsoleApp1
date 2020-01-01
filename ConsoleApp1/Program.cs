using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Services.DefinitionStorage;

namespace ConsoleApp1
{
    public class Program
    {
        private static IWorkflowHost Host;
        public static void Main(string[] args)
        {
            IServiceProvider serviceProvider = ConfigureServices();

            var loader = serviceProvider.GetService<IDefinitionLoader>();
            var json = File.ReadAllText("WorkflowDefinations/driver-corrective-action.json");
            var def = loader.LoadDefinition(json, Deserializers.Json);
            Host = serviceProvider.GetService<IWorkflowHost>();
            Host.Start();
            var workflowId = Host.StartWorkflow(def.Id, new FleetInfo { DriverId = Guid.NewGuid().ToString() }).Result;
            var wf = Host.PersistenceStore.GetWorkflowInstance(workflowId);

            Console.WriteLine("Enter value to publish");
            string value = Console.ReadLine();
            Host.PublishEvent("ExternalEvent", workflowId, value);
            Console.ReadLine();
            Host.Stop();
        }

        private static IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddLogging();
            services.AddWorkflowDSL();
            services.AddWorkflow();
            //services.AddWorkflow(x => x.UseSqlServer(@"Server=.\SQLEXPRESS;Database=WorkflowCore;Integrated Security=SSPI;", true, true));

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
    }


    public class FleetInfo
    {
        public string DriverId { get; set; }

        public int AlertCount { get; set; }

        public DateTime LastEventOccured { get; set; }

        public int AlertThreshold { get; set; }

        public int EscalationLevel { get; set; }

        public DateTime LastNotificationSent { get; set; }

        public TimeSpan NotificationInterval { get; set; }

        public bool CorrectiveActionCompleted { get; set; }
    }
}
