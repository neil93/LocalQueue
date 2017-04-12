using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalQueue
{
    class Program
    {
        static void Main(string[] args)
        {

            var mapList = new List<string>();

            for (int i = 1; i <= 100; i++)
            {
                mapList.Add(i.ToString());
            }

            ProcessManager.Instance.EnqueueMap(mapList);
            Console.WriteLine("doing....");
            Console.ReadKey();

        }
    }
}
