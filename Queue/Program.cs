using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Queue
{
    class Program
    {
        static void Main(string[] args)
        {
            LQueue q1 = new LQueue();

            Task<int> task1 = new Task<int>(() =>  q1.Pop());
            task1.Start();
            Task task2 = new Task(() => q1.Push(5));
            task2.Start();
            
            Console.WriteLine(task1.Result.ToString());
            Console.ReadKey();

        }
        

        class LQueue
        {
            private Queue<int> QueueContainer = new Queue<int>();

            private Object thisLock = new Object();
            public void Push(int a)
            {
                lock (thisLock)
                {
                    QueueContainer.Enqueue(a);
                    Monitor.Pulse(thisLock);
                }
            }
            
            public int Pop()
            {
                lock (thisLock)
                {
                    while (QueueContainer.Count==0)
                    {
                        Monitor.Wait(thisLock);
                    }
                    return QueueContainer.Dequeue();
                }
            }
        }
    }
}
