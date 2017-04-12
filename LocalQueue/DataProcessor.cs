using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace LocalQueue
{
    public class DataProcessor<T>
    {
        private readonly ConcurrentQueue<T> _stores = new ConcurrentQueue<T>();
        private readonly ProcessFlag _status = new ProcessFlag();
        private readonly Action<T> _method;


        public DataProcessor(Action<T> act)
        {
            _method = act;
        }

        public void Enqueue(T context)
        {
            _stores.Enqueue(context);
            
            if (_status.SetInProcess())
            {
                Task.Factory.StartNew(Dequeue);
            }
        }

        private void Dequeue()
        {
            try
            {
                T context;
                while (_stores.TryDequeue(out context))
                {
                    try
                    {
                        _method(context);
                    }
                    catch (Exception ex)
                    {
                        //todo log
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                //todo log
                throw;
            }
            finally
            {
                _status.StopProcess();
            }
        }
    }
}
