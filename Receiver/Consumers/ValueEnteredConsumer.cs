using Carable.MassTransit.BusMessages;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Receiver
{
    public class ValueEnteredConsumer : IConsumer<IValueEntered>
    {
        public Task Consume(ConsumeContext<IValueEntered> context)
        {
            Console.WriteLine("Value entered "+context.Message.Value);
            return Task.FromResult("");
        }
    }
}