namespace SpecificationEngine
{
    public class Message : Result
    {
        public Message(string value, MessageType messageType)
        {
            Value=value;
            MessageType=messageType;
        }
        public string Value { get; }
        public MessageType MessageType { get; }

        public override string ToString()
        {
            return $"[{MessageType}] - {Value}";
        }
    }
}
