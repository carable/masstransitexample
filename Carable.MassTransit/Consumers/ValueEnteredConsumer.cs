using System.Threading.Tasks;
using Carable.MassTransit.BusMessages;
using MassTransit;

namespace Carable.MassTransit.Consumers
{
    public class ValueEnteredConsumer : IConsumer<IValueEntered>
    {
        public Task Consume(ConsumeContext<IValueEntered> context)
        {
            MvcApplication.repository.Add(context.Message);
            return Task.FromResult("");
        }
    }
}