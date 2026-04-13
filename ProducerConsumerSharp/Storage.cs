using System;
using System.Collections.Generic;
using System.Threading;

namespace ProducerConsumerSharp
{
    class Storage
    {
        private readonly Queue<string> buffer = new Queue<string>();
        private readonly int capacity;

        private readonly Semaphore access;
        private readonly Semaphore full;
        private readonly Semaphore empty;

        public Storage(int capacity)
        {
            this.capacity = capacity;
            access = new Semaphore(1, 1);
            full = new Semaphore(capacity, capacity);
            empty = new Semaphore(0, capacity);
        }

        public void Put(string item, int producerId)
        {
            full.WaitOne();
            access.WaitOne();

            buffer.Enqueue(item);
            Console.WriteLine($"[Виробник {producerId}] Додав: {item} | У сховищі: {buffer.Count}/{capacity}");

            access.Release();
            empty.Release();
        }

        public string Take(int consumerId)
        {
            empty.WaitOne();
            access.WaitOne();

            string item = buffer.Dequeue();
            Console.WriteLine($"[Споживач {consumerId}] Взяв:  {item} | У сховищі: {buffer.Count}/{capacity}");

            access.Release();
            full.Release();

            return item;
        }
    }
}
