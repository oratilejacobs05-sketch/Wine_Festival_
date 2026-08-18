using System;
using System.Threading.Tasks;

namespace Wine_Festival_project
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("===== Wine Country Festival Control System =====");

            // Load previously saved data
            if (!FileManager.LoadData())
            {
                Console.WriteLine("No saved data found. Loading starting data.");
                DataStore.SeedData();
            }

            // Run basic tests
            SimpleTests.RunTests();

            // Start background processes
            BackgroundManager.StartBackgroundTasks();

            // Add background tasks
            BackgroundManager.AddToQueue("Generate Sales Report");
            BackgroundManager.AddToQueue("Check Booth Inventory");
            BackgroundManager.AddToQueue("Update Attendance Records");

            await BackgroundManager.WaitForQueueToCompleteAsync();

            Display display = new Display();
            Display.DisplayMenus();

            // Save data when the user exits
            FileManager.SaveData();

            Console.WriteLine("Festival data saved. Goodbye.");
            Console.ReadKey();
        }
    }
}