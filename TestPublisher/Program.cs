using System;
using Configuration;
using Contracts;

namespace TestPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = BusInitializer.CreateBus("TestPublisher", x => { });
            string text = "";

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

            bus.Dispose();
        }
    }
}
