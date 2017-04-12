using System.Threading;

namespace LocalQueue
{
    public class ProcessFlag
    {
        private const int PROCESSING = 1, STOPPED = 0;
        private int usingResource = STOPPED;

        public bool StopProcess()
        {
            return PROCESSING == Interlocked.Exchange(ref usingResource, STOPPED);
        }

        public bool SetInProcess()
        {
            return STOPPED == Interlocked.Exchange(ref usingResource, PROCESSING);
        }

        public bool IsProcessing => Thread.VolatileRead(ref usingResource) == PROCESSING;
    }
}
