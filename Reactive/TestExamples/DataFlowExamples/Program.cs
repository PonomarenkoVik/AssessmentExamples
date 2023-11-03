using DataFlowExamples;
using System.Reactive.Linq;
using System.Threading.Tasks.Dataflow;

namespace FirstExample
{
    public class Program
    {
        static void Main(string[] args)
        {
            //SimpleExample.Execute();
            ProducerConsumerExample.Execute().Wait();
        }

        
    }
}