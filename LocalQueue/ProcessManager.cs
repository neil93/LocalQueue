using System;
using System.Collections.Generic;

namespace LocalQueue
{
    public class ProcessManager
    {
        #region Singleton
        private static readonly Lazy<ProcessManager> _manager = new Lazy<ProcessManager>(() => new ProcessManager());

        private readonly List<DataProcessor<string>> _processors = new List<DataProcessor<string>>();

        public static ProcessManager Instance => _manager.Value;
        #endregion

        private readonly int ThreadCount = 1;

        public ProcessManager()
        {
            for (int i = 0; i < ThreadCount; i++)
            {
                _processors.Add(new DataProcessor<string>(DoMap.Map));
            }
        }

        public void EnqueueMap(IEnumerable<string> mapList)
        {
            foreach (var map in mapList)
            {
                int mapId = 0;
                int queueIndex = 0;
                if (int.TryParse(map, out mapId))
                    queueIndex = mapId % ThreadCount;

                _processors[queueIndex].Enqueue(map);
            }
        }

    }
}
