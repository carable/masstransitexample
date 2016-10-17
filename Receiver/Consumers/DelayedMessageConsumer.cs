using Carable.MassTransit.BusMessages;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Receiver
{
    public class DelayedMessageConsumer : IConsumer<IDelayedMessage>
    {
        public Task Consume(ConsumeContext<IDelayedMessage> context)
        {
            Console.WriteLine("Delayed Value entered " + context.Message.Value);
            return Task.FromResult("");
        }
    }
}