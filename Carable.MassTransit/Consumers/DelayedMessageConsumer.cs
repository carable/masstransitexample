using Carable.MassTransit.BusMessages;
using MassTransit;
using System.Threading.Tasks;

namespace Carable.MassTransit.Consumers
{
    public class DelayedMessageConsumer : IConsumer<IDelayedMessage>
    {
        public Task Consume(ConsumeContext<IDelayedMessage> context)
        {
            MvcApplication.repository.Add(context.Message);
            return Task.FromResult("");
        }
    }
}