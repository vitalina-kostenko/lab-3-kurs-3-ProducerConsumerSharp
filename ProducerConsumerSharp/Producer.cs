using System;
using System.Threading;

namespace ProducerConsumerSharp
{
    class Producer
    {
        private readonly int id;
        private readonly Storage storage;
        private readonly SharedCounter counter;

        public Producer(int id, Storage storage, SharedCounter counter)
        {
            this.id = id;
            this.storage = storage;
            this.counter = counter;
        }

        public void Run()
        {
            while (true)
            {
                int itemNumber = counter.GetNextProduced();
                if (itemNumber < 0) break;

                string item = $"продукція #{itemNumber}";
                Thread.Sleep(new Random().Next(100, 500));
                storage.Put(item, id);
            }
        }
    }
}
