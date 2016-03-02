using System;
using MassTransit;
using MassTransit.Log4NetIntegration.Logging;
using MassTransit.Pipeline;

namespace TestSubscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            Log4NetLogger.Use();
            var bus = Bus.Factory.CreateUsingRabbitMq(x =>
            {
                var host = x.Host(new Uri("rabbitmq://localhost/"), h => { });
                x.UseConcurrencyLimit(1);
                x.PrefetchCount = 1;
                x.ReceiveEndpoint(host, "MtPubSubExample_TestSubscriber", e =>
                  {
                      e.UseConcurrencyLimit(1);
                      e.PrefetchCount = 1;
                      var consumer = new SomethingHappenedConsumer();
                      e.Consumer(() => consumer);
                  }
            );
            });
            var busHandle = bus.Start();
            Console.ReadKey();
            busHandle.Stop();
        }
    }
}
