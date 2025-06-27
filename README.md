# OfflineSyncMaui
# Queue network requests and sync when online
- Queue API requests (POST/PUT/DELETE) when offline
- Automatically sync them once the device is back online
- Works even after app restart (using local storage)

| Feature             | Implementation                                           |
| ------------------- | -------------------------------------------------------- |
| Detect connectivity | `Connectivity.ConnectivityChanged` (MAUI Essentials)     |
| Queue requests      | Save requests locally using SQLite, LiteDB, or JSON file |
| Background sync     | Trigger a retry when connectivity returns                |
| HTTP handler        | Use `HttpClient` to actually send queued requests        |



| Component             | Responsibility                     |
| --------------------- | ---------------------------------- |
| `RequestQueueStorage` | Store and load queued requests     |
| `SendOrQueueAsync`    | Queue if offline, send if online   |
| `ProcessQueueAsync`   | Replay queued requests when online |
| `ConnectivityChanged` | Trigger replay logic automatically |
