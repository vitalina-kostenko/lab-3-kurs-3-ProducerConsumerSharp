using System;
using System.Threading;

namespace ProducerConsumerSharp
{
    class Consumer
    {
        private readonly int id;
        private readonly Storage storage;
        private readonly SharedCounter counter;

        public Consumer(int id, Storage storage, SharedCounter counter)
        {
            this.id = id;
            this.storage = storage;
            this.counter = counter;
        }

        public void Run()
        {
            while (true)
            {
                int itemNumber = counter.GetNextConsumed();
                if (itemNumber < 0) break;

                storage.Take(id);
                Thread.Sleep(new Random().Next(200, 800));
            }
        }
    }
}
