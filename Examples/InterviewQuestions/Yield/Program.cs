using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yield
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var el = Method().Take(3);

            foreach (var item in el)
            {
                Console.WriteLine(item);
            }
        }


        public static IEnumerable<int> Method1()
        {
            try
            {
                while (true)
                {
                    yield return 1;
                }
            }
            finally
            {
                Console.WriteLine("Dispose");
            }
        }

        public static IEnumerable<int> Method()
        {
            try
            {
                int i = 0;
                yield return ++i;
                yield return ++i;
                yield return ++i;
            }
            finally
            {
                Console.WriteLine("Dispose");
            }
        }
    }
}
