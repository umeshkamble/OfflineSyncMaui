using OfflineSyncMauiSolution.Models;
using OfflineSyncMauiSolution.Storage;
using System.Text;
using System.Text.Json;

namespace OfflineSyncMauiSolution.Services
{
    public static class SyncService
    {
        public static async Task SendOrQueueAsync(string url, HttpMethod method, object data)
        {
            var json = JsonSerializer.Serialize(data);

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await RequestQueueStorage.AddAsync(new QueuedRequest
                {
                    Url = url,
                    Method = method.Method,
                    Body = json
                });
                return;
            }

            await SendHttpRequestAsync(url, method, json);
        }


        
        public static async Task ProcessQueueAsync()
        {
            var queue = await RequestQueueStorage.LoadAsync();

            foreach (var request in queue.ToList())
            {
                try
                {
                    await SendHttpRequestAsync(request.Url, new HttpMethod(request.Method), request.Body);
                    await RequestQueueStorage.RemoveAsync(request.Id);
                }
                catch
                {
                }
            }
        }


        public static async Task SendHttpRequestAsync(string url, HttpMethod method, string jsonBody)
        {
            using var client = new HttpClient();
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage resp = method switch
            {
                var m when m == HttpMethod.Post => await client.PostAsync(url, content),
                var m when m == HttpMethod.Put => await client.PutAsync(url, content),
                var m when m == HttpMethod.Delete => await client.DeleteAsync(url),
                _ => throw new NotSupportedException()
            };
            resp.EnsureSuccessStatusCode();
        }
    }

}
