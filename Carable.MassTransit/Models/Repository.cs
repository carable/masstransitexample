using Carable.MassTransit.BusMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Carable.MassTransit.Models
{
    public class Repository
    {
        List<IValueEntered> messages = new List<IValueEntered>();
        List<IDelayedMessage> delayedMessages = new List<IDelayedMessage>();
        public Repository()
        {
        }
        public void Add(IValueEntered val) { messages.Add(val); }
        public void Add(IDelayedMessage val) { delayedMessages.Add(val); }
        public ReceivedMessages List()
        {
            return new ReceivedMessages(messages, delayedMessages);
        }
    }
}