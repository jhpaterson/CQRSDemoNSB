using NServiceBus;
using CQRSDemo.Web.Injection;

namespace CQRSDemo.Web.Injection
{
    public class NServiceBusConfig
    {
        public static void RegisterNServiceBus()
        {
            // NServiceBus configuration
            Configure.With()
                .DefaultBuilder()
                .ForMvc()
                .JsonSerializer()
                .Log4Net()
                .MsmqTransport()
                    .IsTransactional(false)
                    .PurgeOnStartup(true)
                .UnicastBus()
                    .ImpersonateSender(false)
                    .SendOnly();
                //.CreateBus()
                //.Start();
        }
    }
}