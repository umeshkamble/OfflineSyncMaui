using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineSyncMauiSolution.Models
{
    public class QueuedRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Url { get; set; }
        public string Method { get; set; } // "POST", "PUT", etc.
        public string Body { get; set; }   // Serialized JSON
        public DateTime QueuedAt { get; set; } = DateTime.UtcNow;
    }

}
