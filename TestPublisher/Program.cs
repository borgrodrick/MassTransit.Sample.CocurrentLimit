using System;
using System.Threading;
using Contracts;
using MassTransit;
using MassTransit.Log4NetIntegration.Logging;

namespace TestPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            Log4NetLogger.Use();
            var bus = Bus.Factory.CreateUsingRabbitMq(x =>
            {
                x.Host(new Uri("rabbitmq://localhost/"), h => { });
            }
              );
            var busHandle = bus.Start();
            var text = "";

            while (text != "quit")
            {
                Console.Write("Enter a message: ");
                text = Console.ReadLine();

                for (int i = 0; i <= 1000; i++)
                {
                    Console.WriteLine(i);
                    var message = new SomethingHappenedMessage()
                    {
                        What = i.ToString(),
                        When = DateTime.Now
                    };
                    bus.Publish<SomethingHappened>(message);
                }

            }

            busHandle.Stop();
        }
    }
}
