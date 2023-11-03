using System.Threading.Tasks.Dataflow;

namespace DataFlowExamples
{
    internal class ProducerConsumerExample
    {
        static void Produce(ITargetBlock<byte[]> target)
        {
            var rand = new Random();

            for (int i = 0; i < 100; ++i)
            {
                var buffer = new byte[1024];
                rand.NextBytes(buffer);
                target.Post(buffer);
            }

            target.Complete();
        }

        static async Task<int> ConsumeAsync(ISourceBlock<byte[]> source)
        {
            int bytesProcessed = 0;

            while (await source.OutputAvailableAsync())
            {
                byte[] data = await source.ReceiveAsync();
                bytesProcessed += data.Length;
            }

            return bytesProcessed;
        }


        public static async Task Execute()
        {
            var buffer = new BufferBlock<byte[]>();
            var consumerTask = ConsumeAsync(buffer);
            Produce(buffer);

            var bytesProcessed = await consumerTask;

            Console.WriteLine($"Processed {bytesProcessed:#,#} bytes.");
        }

        static async Task<int> ConsumeAsyncWithTryReceive(IReceivableSourceBlock<byte[]> source)
        {
            int bytesProcessed = 0;
            while (await source.OutputAvailableAsync())
            {
                while (source.TryReceive(out byte[] data))
                {
                    bytesProcessed += data.Length;
                }
            }
            return bytesProcessed;
        }
    }
}
