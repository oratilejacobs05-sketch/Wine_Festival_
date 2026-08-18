using System;
using System.IO;

namespace Wine_Festival_project
{
    public static class FileManager
    {
        private const string FilePath = "festival_data.txt";

        // Save festival data
        public static void SaveData()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(FilePath, false))
                {
                    // Wine booths
                    foreach (Winebooth booth in DataStore.WineBooths)
                    {
                        writer.WriteLine(
                            "BOOTH|" +
                            Clean(booth.Name) + "|" +
                            Clean(booth.VendorName) + "|" +
                            Clean(booth.WineVarietal) + "|" +
                            booth.TotalBottles + "|" +
                            booth.BottlesSold + "|" +
                            booth.IsActive);
                    }

                    // Attendees
                    foreach (attendees attendee in DataStore.Attendees)
                    {
                        writer.WriteLine(
                            "ATTENDEE|" +
                            Clean(attendee.Name) + "|" +
                            Clean(attendee.TicketNumber) + "|" +
                            attendee.Age + "|" +
                            attendee.IsVIP + "|" +
                            attendee.IsCheckedIn + "|" +
                            attendee.IsActive);
                    }

                    // Wine stock
                    foreach (WineStock stock in DataStore.WineStocks)
                    {
                        writer.WriteLine(
                            "STOCK|" +
                            Clean(stock.Name) + "|" +
                            Clean(stock.WineName) + "|" +
                            stock.VintageYear + "|" +
                            stock.QuantityLiters + "|" +
                            stock.IsActive);
                    }
                }

                Console.WriteLine("Festival data saved.");
                Logger.Log("Festival data saved.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not save festival data.");
                Logger.Log("Save failed: " + ex.Message);
            }
        }

        // Load festival data
        public static bool LoadData()
        {
            if (!File.Exists(FilePath))
            {
                return false;
            }

            try
            {
                DataStore.WineBooths.Clear();
                DataStore.Attendees.Clear();
                DataStore.WineStocks.Clear();

                string[] lines = File.ReadAllLines(FilePath);

                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    string[] parts = line.Split('|');

                    // BOOTH
                    if (parts.Length >= 7 && parts[0] == "BOOTH")
                    {
                        int totalBottles;
                        int bottlesSold;
                        bool isActive;

                        if (!int.TryParse(parts[4], out totalBottles) ||
                            !int.TryParse(parts[5], out bottlesSold) ||
                            !bool.TryParse(parts[6], out isActive))
                        {
                            continue;
                        }

                        Winebooth booth = new Winebooth(
                            parts[1],
                            parts[2],
                            parts[3],
                            totalBottles);

                        if (bottlesSold > 0)
                        {
                            booth.Consume(bottlesSold);
                        }

                        booth.IsActive = isActive;

                        DataStore.WineBooths.Add(booth);
                    }

                    // ATTENDEE
                    else if (parts.Length >= 7 && parts[0] == "ATTENDEE")
                    {
                        string[] nameParts = parts[1].Split(
                            new char[] { ' ' },
                            2,
                            StringSplitOptions.RemoveEmptyEntries);

                        string firstName = "Unknown";
                        string surname = "Unknown";

                        if (nameParts.Length > 0)
                        {
                            firstName = nameParts[0];
                        }

                        if (nameParts.Length > 1)
                        {
                            surname = nameParts[1];
                        }

                        int age;
                        bool isVip;
                        bool isCheckedIn;
                        bool isActive;

                        if (!int.TryParse(parts[3], out age) ||
                            !bool.TryParse(parts[4], out isVip) ||
                            !bool.TryParse(parts[5], out isCheckedIn) ||
                            !bool.TryParse(parts[6], out isActive))
                        {
                            continue;
                        }

                        attendees attendee = new attendees(
                            firstName,
                            surname,
                            parts[2],
                            age,
                            isVip);

                        attendee.IsCheckedIn = isCheckedIn;
                        attendee.IsActive = isActive;

                        DataStore.Attendees.Add(attendee);
                    }

                    // STOCK
                    else if (parts.Length >= 6 && parts[0] == "STOCK")
                    {
                        int vintageYear;
                        int quantity;
                        bool isActive;

                        if (!int.TryParse(parts[3], out vintageYear) ||
                            !int.TryParse(parts[4], out quantity) ||
                            !bool.TryParse(parts[5], out isActive))
                        {
                            continue;
                        }

                        WineStock stock = new WineStock(
                            parts[1],
                            parts[2],
                            vintageYear,
                            quantity);

                        stock.IsActive = isActive;

                        DataStore.WineStocks.Add(stock);
                    }
                }

                Console.WriteLine("Festival data loaded.");
                Logger.Log("Festival data loaded.");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not load festival data.");
                Logger.Log("Load failed: " + ex.Message);

                return false;
            }
        }

        private static string Clean(string value)
        {
            if (value == null)
            {
                return "";
            }

            return value.Replace("|", "/");
        }
    }
}