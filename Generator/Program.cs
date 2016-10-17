using System;
using MassTransit;
using Carable.MassTransit.BusMessages;

namespace Generator
{
    class Program
    {
        private static IBusControl configureBus()
        {
            return Bus.Factory.CreateUsingRabbitMq((cfg) =>
            {
                cfg.Host(new Uri("rabbitmq://localhost"), (h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                }));
                cfg.UseInMemoryScheduler();
            });
        }

        static void Main(string[] args)
        {
            var busControl = configureBus();
            var run = true;
            using (var h = busControl.Start())
            {
                Console.WriteLine("Enter message (or ctrl+c to exit)");
                try
                {
                    while (run)
                    {
                        Console.WriteLine("> ");
                        var value = Console.ReadLine();
                        if (!String.IsNullOrEmpty(value))
                        {
                            var sendEndpoint = busControl.GetSendEndpoint(new Uri("rabbitmq://localhost/quartz"));
                            var s = sendEndpoint.Result.ScheduleSend<IDelayedMessage>(new Uri("rabbitmq://localhost/service_queue"),
                                                                    TimeSpan.FromSeconds(1),
                                                                            Messages.CreateDelayedMessage(value), System.Threading.CancellationToken.None);
                            s.Wait();
                            var s2 = busControl.Publish<IValueEntered>(Messages.CreateValueEntered(value));
                            s2.Wait();
                            Console.WriteLine("published " + value);
                        }
                    }
                }
                finally
                {
                    busControl.Stop();
                }
            }
        }
    }
}
