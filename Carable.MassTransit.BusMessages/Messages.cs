namespace Carable.MassTransit.BusMessages
{
    public interface IValueEntered
    {
        string Value { get; }
    }
    public interface IDelayedMessage
    {
        string Value { get; }
    }
    public static class Messages
    {
        private class ValueEntered : IValueEntered
        {
            public string Value { get; set; }
        }
        private class DelayedMessage:IDelayedMessage
        {
            public string Value { get; set; }
        }
        public static IValueEntered CreateValueEntered(string value)
        {
            return new ValueEntered { Value = value };
        }
        public static IDelayedMessage CreateDelayedMessage(string value)
        {
            return new DelayedMessage { Value = value };
        }
    }
}
