using MassTransit;
using MassTransit.BusConfigurators;
using MassTransit.Log4NetIntegration.Logging;
using System;

namespace Configuration
{
  public class BusInitializer
  {
    public static IServiceBus CreateBus(string queueName, Action<ServiceBusConfigurator> moreInitialization)
    {
      Log4NetLogger.Use();
      var bus = ServiceBusFactory.New(x =>
      {
          x.SetConcurrentConsumerLimit(10);
        x.UseRabbitMq();
        x.ReceiveFrom("rabbitmq://localhost/MtPubSubExampleV2_" + queueName);
        moreInitialization(x);
      });

      return bus;
    }
  }
}
