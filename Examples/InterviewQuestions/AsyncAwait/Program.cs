using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait
{
    public class Program
    {
        internal static void Main(string[] args)
        {
            RunAsync();
        }

        public static async void RunAsync()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;

            try
            {
                ExceptionAsync();
            }
            catch (Exception ex)
            {

            }

            //var res1 = await Method1Async();
            //var res2 = Method2Async();

            while (true)
            {
                System.Diagnostics.Debug.WriteLine("Cycle");
                await Task.Delay(1000);
                //Thread.Sleep(100);
            }
        }

        private static void CurrentDomain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
        {
      
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //e.IsTerminating = false;
        }

        public static async Task<int> Method1Async()
        {
            int i = 0;
            System.Diagnostics.Debug.WriteLine($"Method1Async:: part1:: ManagedThreadId: {Thread.CurrentThread.ManagedThreadId}");
            await Task.Run(() =>
            {
                System.Diagnostics.Debug.WriteLine($"Method1Async:: part2:: ManagedThreadId: {Thread.CurrentThread.ManagedThreadId}");
                i++;
            });

            
            System.Diagnostics.Debug.WriteLine($"Method1Async:: part3:: ManagedThreadId: {Thread.CurrentThread.ManagedThreadId}");
            i++;
            return i;
        }

        public static int Method2Async()
        {
            System.Diagnostics.Debug.WriteLine($"Method2Async:: part1:: ManagedThreadId: {Thread.CurrentThread.ManagedThreadId}");
            int i = 0;
            Task.Run(() => 
            {
                Thread.Sleep(1000);
                System.Diagnostics.Debug.WriteLine($"Method2Async:: part2:: ManagedThreadId: {Thread.CurrentThread.ManagedThreadId}");
                i++; 
            });

            System.Diagnostics.Debug.WriteLine($"Method2Async:: part3:: ManagedThreadId: {Thread.CurrentThread.ManagedThreadId}");
            i++;
            return i;
        }



        public static async void ExceptionAsync()
        {
            System.Diagnostics.Debug.WriteLine($"ExceptionAsync:: part1:: ManagedThreadId: {Thread.CurrentThread.ManagedThreadId}");
            await Task.Run(() =>
            {

                System.Diagnostics.Debug.WriteLine($"ExceptionAsync:: part2:: ManagedThreadId: {Thread.CurrentThread.ManagedThreadId}");
                throw new FormatException();

            });

            System.Diagnostics.Debug.WriteLine($"ExceptionAsync:: part3:: ManagedThreadId: {Thread.CurrentThread.ManagedThreadId}");


        }

    }
}
