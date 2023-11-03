using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TaskComplitionSource
{
    public interface IMyAsyncHttpService
    {
        void DownloadString(Uri address, Action<string, Exception> callback);
    }

    public static class TaskComplitionSourceExtensions
    {
        public static Task<string> DownloadStringAsync(this IMyAsyncHttpService httpService, Uri address)
        {
            var tcs = new TaskCompletionSource<string>();
            httpService.DownloadString(address, (result, exception) =>
            {
                if (exception != null)
                    tcs.TrySetException(exception);
                else
                    tcs.TrySetResult(result);
            });
            return tcs.Task;
        }

    }
}
