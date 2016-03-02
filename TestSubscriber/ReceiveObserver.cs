using System;
using System.Threading.Tasks;
using MassTransit;

namespace TestSubscriber
{
    class ReceiveObserver: IReceiveObserver
    {
        public Task PreReceive(ReceiveContext context)
        {
           Console.WriteLine("Delivered " + context.GetBody());
            return Task.FromResult(0);
        }

        public Task PostReceive(ReceiveContext context)
        {
            Console.WriteLine("Received and Processed " + context.GetBody() );
            return Task.FromResult(0);
        }

        public Task PostConsume<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType) where T : class
        {
            Console.WriteLine("Consumed by " + consumerType);
            return Task.FromResult(0);
        }

        public Task ConsumeFault<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType, Exception exception) where T : class
        {
            Console.WriteLine("Consumed by " + consumerType + " consumer throws an exception");
            return Task.FromResult(0);
        }

        public Task ReceiveFault(ReceiveContext context, Exception exception)
        {
            Console.WriteLine("exception occurs early in the message processing, such as deserialization, etc.");
            return Task.FromResult(0);
        }
    }
}
