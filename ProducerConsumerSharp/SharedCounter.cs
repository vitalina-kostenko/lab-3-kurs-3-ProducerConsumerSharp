using System.Threading;

namespace ProducerConsumerSharp
{
    class SharedCounter
    {
        private readonly int totalItems;
        private int produced = 0;
        private int consumed = 0;
        private readonly object lockObj = new object();

        public SharedCounter(int totalItems)
        {
            this.totalItems = totalItems;
        }

        public int GetNextProduced()
        {
            lock (lockObj)
            {
                if (produced >= totalItems) return -1;
                return produced++;
            }
        }

        public int GetNextConsumed()
        {
            lock (lockObj)
            {
                if (consumed >= totalItems) return -1;
                return consumed++;
            }
        }

        public int Consumed
        {
            get { lock (lockObj) { return consumed; } }
        }

        public int Produced
        {
            get { lock (lockObj) { return produced; } }
        }
    }
}
