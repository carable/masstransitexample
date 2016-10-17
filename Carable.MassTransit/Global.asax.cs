using Carable.MassTransit.Consumers;
using Carable.MassTransit.Models;
using MassTransit;
using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Carable.MassTransit
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            repository = new Repository();
            _busControl = ConfigureBus();
            _busControl.Start();
        }
        static IBusControl _busControl;
        public static Repository repository;

        public static IBus Bus
        {
            get { return _busControl; }
        }


        protected void Application_End()
        {
            _busControl.Stop();
        }

        IBusControl ConfigureBus()
        {
            return global::MassTransit.Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.UseDelayedExchangeMessageScheduler();
                cfg.ReceiveEndpoint(host, "service_queue", e =>
                {
                    e.Consumer<ValueEnteredConsumer>();
                    e.Consumer<DelayedMessageConsumer>();
                });
            });
        }
    }
}
