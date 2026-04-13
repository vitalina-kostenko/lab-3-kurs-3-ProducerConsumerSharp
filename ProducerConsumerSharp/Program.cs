using System;
using System.Threading;

namespace ProducerConsumerSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Кількість виробників: ");
            int producerCount = int.Parse(Console.ReadLine()!);

            Console.Write("Кількість споживачів: ");
            int consumerCount = int.Parse(Console.ReadLine()!);

            Console.Write("Максимальна місткість сховища: ");
            int storageCapacity = int.Parse(Console.ReadLine()!);

            Console.Write("Загальна кількість продукції: ");
            int totalItems = int.Parse(Console.ReadLine()!);

            Console.WriteLine($"\n--- Параметри ---");
            Console.WriteLine($"Виробників: {producerCount}");
            Console.WriteLine($"Споживачів: {consumerCount}");
            Console.WriteLine($"Місткість сховища: {storageCapacity}");
            Console.WriteLine($"Кількість продукції: {totalItems}");
            Console.WriteLine($"-----------------\n");

            var storage = new Storage(storageCapacity);
            var counter = new SharedCounter(totalItems);

            var producers = new Thread[producerCount];
            for (int i = 0; i < producerCount; i++)
            {
                var producer = new Producer(i, storage, counter);
                producers[i] = new Thread(producer.Run);
                producers[i].Start();
            }

            var consumers = new Thread[consumerCount];
            for (int i = 0; i < consumerCount; i++)
            {
                var consumer = new Consumer(i, storage, counter);
                consumers[i] = new Thread(consumer.Run);
                consumers[i].Start();
            }

            foreach (var t in producers) t.Join();
            foreach (var t in consumers) t.Join();

            Console.WriteLine($"\n--- Результат ---");
            Console.WriteLine($"Вироблено: {counter.Produced}");
            Console.WriteLine($"Спожито:   {counter.Consumed}");
            Console.WriteLine($"-----------------");
        }
    }
}
