# dotnetcore
Basic Net core projects

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Async
{
    public class Opearations
    {
        public async Task DoOperation0Async()
        {
            Console.WriteLine("start operation 0 ");
            await Task.Delay(8000);
            Console.WriteLine("finished operation 0 ");
        }

        public async Task DoOperation1Async()
        {
            Console.WriteLine("start operation 1 ");
            await Task.Delay(6000);
            Console.WriteLine("finished operation 1 ");
        }
        public async Task DoOperation2Async()
        {
            Console.WriteLine("start operation 2 ");
            await Task.Delay(10000);
            Console.WriteLine("finished operation 2 ");
        }


        public async Task proccesingAsync()
        {
            Stopwatch stopWatch = new Stopwatch();
           
            Console.WriteLine("--------------------start proccesing");
            stopWatch.Start();
            Task[] tasks = new Task[3];
            tasks[0] = DoOperation0Async();
            tasks[1] = DoOperation1Async();
            tasks[2] = DoOperation2Async();

            // At this point, all three tasks are running at the same time.

            // Now, we await them all.

            
            await Task.WhenAll(tasks);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Console.WriteLine("--------------------finished proccesing :" + elapsedTime);
        }
    }
    class Program
    {
        static async Task Main(string[] args)
        {
            var Oper = new Opearations();
           await  Oper.proccesingAsync();

            Console.WriteLine("end game");
            Thread.Sleep(10000);
        }
    }
}
