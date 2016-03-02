using System;
using System.Runtime.Remoting.Channels;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
using MassTransit;

namespace TestSubscriber
{
    class SomethingHappenedConsumer : Consumes<SomethingHappened>.Context
    {
        private static readonly Random rand = new Random();

        public void Consume(IConsumeContext<SomethingHappened> context)
        {
            var randomTime = rand.Next(0, 1000);
            Console.WriteLine("Number is : " +
                              context.Message.What + " time to slp:" + randomTime + " Thread no : " +
                              Thread.CurrentThread.ManagedThreadId + " Sent: " +
                              context.Message.When.ToString("H:mm:ss") +
                              context.Message.When.Millisecond);
            Thread.Sleep(randomTime);
        }
    }
}
