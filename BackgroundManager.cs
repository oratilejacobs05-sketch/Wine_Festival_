using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wine_Festival_project
{
    internal class BackgroundManager
    {
        private static readonly object _lockObject =
            new object();

        private static readonly Queue<string> processingQueue =
            new Queue<string>();

        private static readonly HashSet<string> reportedVIPs =
            new HashSet<string>();

        private static bool _isProcessingTask = false;

        private static readonly Random random =
            new Random();

        // Start background processes
        public static void StartBackgroundTasks()
        {
            Task.Run(() => MonitoringLoop());

            Task.Run(() => ProcessQueue());

            Task.Run(() => RandomFailureGenerator());

            Logger.Log(
                "Background processes started.");
        }

        // Add task to queue
        public static void AddToQueue(string task)
        {
            lock (_lockObject)
            {
                processingQueue.Enqueue(task);
            }

            Console.WriteLine(
                "Task Added: " + task);

            Logger.Log(
                "Background task added: " + task);
        }

        // Wait until queue is empty
        public static async Task WaitForQueueToCompleteAsync()
        {
            while (true)
            {
                lock (_lockObject)
                {
                    if (processingQueue.Count == 0 &&
                        !_isProcessingTask)
                    {
                        return;
                    }
                }

                await Task.Delay(200);
            }
        }

        // Process queue
        private static async Task ProcessQueue()
        {
            while (true)
            {
                string currentTask = null;

                lock (_lockObject)
                {
                    if (processingQueue.Count > 0)
                    {
                        currentTask =
                            processingQueue.Dequeue();

                        _isProcessingTask = true;
                    }
                }

                if (currentTask != null)
                {
                    Console.WriteLine(
                        "Processing Queue Task: " +
                        currentTask);

                    await Task.Delay(3000);

                    lock (_lockObject)
                    {
                        _isProcessingTask = false;
                    }

                    Logger.Log(
                        "Background task completed: " +
                        currentTask);
                }
                else
                {
                    await Task.Delay(500);
                }
            }
        }

        // Monitor festival
        private static async Task MonitoringLoop()
        {
            while (true)
            {
                lock (DataStore.SyncRoot)
                {
                    foreach (Winebooth booth
                        in DataStore.WineBooths)
                    {
                        if (booth.CurrentStock < 20)
                        {
                            Console.WriteLine(
                                "WARNING: " +
                                booth.Name +
                                " has low stock.");

                            Logger.Log(
                                "Low stock warning: " +
                                booth.Name);
                        }
                    }

                    foreach (attendees attendee
                        in DataStore.Attendees)
                    {
                        if (attendee.IsVIP &&
                            !attendee.IsCheckedIn &&
                            !reportedVIPs.Contains(
                                attendee.Id))
                        {
                            Console.WriteLine(
                                "VIP attendee " +
                                attendee.Name +
                                " has not checked in.");

                            reportedVIPs.Add(
                                attendee.Id);

                            Logger.Log(
                                "VIP attendee has not checked in: " +
                                attendee.Name);
                        }
                    }
                }

                await Task.Delay(5000);
            }
        }

        // Random failure simulation
        private static async Task RandomFailureGenerator()
        {
            await Task.Delay(1000);

            while (true)
            {
                lock (DataStore.SyncRoot)
                {
                    if (DataStore.WineBooths.Count > 0)
                    {
                        int index =
                            random.Next(
                                DataStore.WineBooths.Count);

                        Winebooth booth =
                            DataStore.WineBooths[index];

                        if (booth.IsActive)
                        {
                            booth.IsActive = false;

                            Console.WriteLine(
                                "ALERT: " +
                                booth.Name +
                                " experienced a temporary failure!");

                            Logger.Log(
                                "Background alert - booth failure: " +
                                booth.Name);
                        }
                    }
                }

                // Wait before checking for another failure
                await Task.Delay(10000);
            }
        }

        public static void WaitAll()
        {
            // Kept for compatibility with the existing project.
        }
    }
}