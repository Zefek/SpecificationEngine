using System;
using System.Linq;
using System.Threading.Tasks;

namespace SpecificationEngine
{
    public class ErrorMessage
    {
        private Delegate errorMessageFuction;
        private ErrorMessage(Delegate errorMessageFunction)
        {
            this.errorMessageFuction = errorMessageFunction ?? throw new ArgumentNullException(nameof(errorMessageFuction));
        }

        internal MessageType MessageType { get; }

        public static ErrorMessage Create(string errorMessage, MessageType messageType) => new ErrorMessage(() => new Message(errorMessage, messageType));

        public static ErrorMessage Create<T>(Func<T, Message> errorMessageFunction) => new ErrorMessage(errorMessageFunction);

        public static ErrorMessage Create<T>(Func<T, Task<Message>> errorMessageFunction) => new ErrorMessage(errorMessageFunction);

        public async Task<Message> GetMessage(object input)
        {
            dynamic? result;
            if (!errorMessageFuction.Method.GetParameters().Any())
            {
                result = errorMessageFuction.DynamicInvoke();
            }
            else
            {
                result = errorMessageFuction.DynamicInvoke(input);
            }
            if (result is Task<Message> task)
            {
                return await task;
            }
            if (result is Message value)
            {
                return value;
            }
            throw new NotSupportedException("Result type "+(result == null ? "null" : result.GetType().FullName)+" is not supported as result type of error message function.");
        }
    }
}
