using Carable.MassTransit.BusMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Carable.MassTransit.Models
{
    public class ReceivedMessages
    {
        public readonly IValueEntered[] Values;

        public readonly IDelayedMessage[] Delayed;

        public ReceivedMessages(IEnumerable<IValueEntered> values, IEnumerable<IDelayedMessage> delayed)
        {
            Values = values.ToArray();
            Delayed = delayed.ToArray();
        }
    }
}