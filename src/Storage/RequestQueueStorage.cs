using OfflineSyncMauiSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OfflineSyncMauiSolution.Storage
{
    public static class RequestQueueStorage
    {
        private static readonly string FilePath = Path.Combine(FileSystem.AppDataDirectory, "requestQueue.json");

        public static async Task<List<QueuedRequest>> LoadAsync()
        {
            if (!File.Exists(FilePath)) return new List<QueuedRequest>();
            var json = await File.ReadAllTextAsync(FilePath);
            return JsonSerializer.Deserialize<List<QueuedRequest>>(json) ?? new();
        }

        public static async Task SaveAsync(List<QueuedRequest> queue)
        {
            var json = JsonSerializer.Serialize(queue);
            await File.WriteAllTextAsync(FilePath, json);
        }

        public static async Task AddAsync(QueuedRequest request)
        {
            var queue = await LoadAsync();
            queue.Add(request);
            await SaveAsync(queue);
        }

        public static async Task RemoveAsync(Guid requestId)
        {
            var queue = await LoadAsync();
            queue.RemoveAll(q => q.Id == requestId);
            await SaveAsync(queue);
        }
    }

}
