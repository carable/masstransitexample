using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receiver
{
    class Program
    {
        private static IBusControl configureBus()
        {
            return Bus.Factory.CreateUsingRabbitMq((cfg) =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost"), (h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                }));
                cfg.ReceiveEndpoint(host, "subscriber", e => {
                    e.Consumer<ValueEnteredConsumer>();
                    e.Consumer<DelayedMessageConsumer>();
                });
            });
        }

        static void Main(string[] args)
        {
            var busControl = configureBus();
            var run = true;
            using (var h = busControl.Start())
            {
                Console.WriteLine("Press something to finish");
                try
                {
                    Console.ReadLine();
                }
                finally
                {
                    busControl.Stop();
                }
            }
        }
    }
}
