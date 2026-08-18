using System;
using System.Linq;

namespace Wine_Festival_project
{
    public static class FestivalService
    {
        // Add booth
        public static void AddBooth(
            string boothName,
            string vendor,
            string varietal,
            int bottles)
        {
            if (bottles <= 0)
            {
                throw new ArgumentException(
                    "Number of bottles must be greater than zero.");
            }

            Winebooth booth =
                new Winebooth(
                    boothName,
                    vendor,
                    varietal,
                    bottles);

            lock (DataStore.SyncRoot)
            {
                DataStore.WineBooths.Add(booth);
            }

            Console.WriteLine(
                $"Booth: '{boothName}' added with ID: {booth.Id}");

            Logger.Log(
                $"Created booth: {booth.Name} ({booth.Id})");
        }

        // Register attendee
        public static void RegisterAttendee(
            string firstName,
            string surname,
            string ticket,
            int age,
            bool isVip)
        {
            if (age < 18)
            {
                throw new Exception(
                    "Attendee must be 18 or older to register.");
            }

            lock (DataStore.SyncRoot)
            {
                int currentCheckedIn =
                    DataStore.Attendees.Count(
                        a => a.IsCheckedIn);

                if (currentCheckedIn >=
                    DataStore.MAX_FESTIVAL_CAPACITY)
                {
                    throw new FestivalCapacityException(
                        currentCheckedIn,
                        DataStore.MAX_FESTIVAL_CAPACITY);
                }

                if (isVip)
                {
                    int currentVip =
                        DataStore.Attendees.Count(
                            a => a.IsVIP &&
                                 a.IsCheckedIn);

                    if (currentVip >=
                        DataStore.MAX_VIP_CAPACITY)
                    {
                        throw new Exception(
                            $"VIP lounge full. Only {DataStore.MAX_VIP_CAPACITY} VIPs allowed inside.");
                    }
                }

                attendees attendee =
                    new attendees(
                        firstName,
                        surname,
                        ticket,
                        age,
                        isVip);

                DataStore.Attendees.Add(
                    attendee);

                Console.WriteLine(
                    $"Attendee '{firstName} {surname}' registered with ID: {attendee.Id}");

                Logger.Log(
                    $"Created attendee: {attendee.Name} ({attendee.Id})");
            }
        }

        // Add wine stock
        public static void AddWineStock(
            string stockName,
            string varietal,
            int year,
            int liters)
        {
            if (liters <= 0)
            {
                throw new ArgumentException(
                    "Wine stock quantity must be greater than zero.");
            }

            WineStock stock =
                new WineStock(
                    stockName,
                    varietal,
                    year,
                    liters);

            lock (DataStore.SyncRoot)
            {
                DataStore.WineStocks.Add(stock);
            }

            Console.WriteLine(
                $"Stock: '{stockName}' added with ID: {stock.Id}");

            Logger.Log(
                $"Created wine stock: {stock.Name} ({stock.Id})");
        }

        // Check in attendee
        public static void CheckInAttendee(
            string ticketNumber)
        {
            lock (DataStore.SyncRoot)
            {
                attendees attendee =
                    DataStore.Attendees.FirstOrDefault(
                        a => a.TicketNumber == ticketNumber);

                if (attendee == null)
                {
                    throw new ArgumentException(
                        "Ticket not found.");
                }

                if (attendee.IsCheckedIn)
                {
                    throw new InvalidOperationException(
                        "Already checked in.");
                }

                int currentCheckedIn =
                    DataStore.Attendees.Count(
                        a => a.IsCheckedIn);

                if (currentCheckedIn >=
                    DataStore.MAX_FESTIVAL_CAPACITY)
                {
                    throw new FestivalCapacityException(
                        currentCheckedIn,
                        DataStore.MAX_FESTIVAL_CAPACITY);
                }

                attendee.IsCheckedIn = true;

                Console.WriteLine(
                    $"{attendee.Name} checked in. ID: {attendee.Id}");

                Logger.Log(
                    $"Checked in attendee: {attendee.Name} ({attendee.Id})");
            }
        }

        // Sell wine
        public static void SellWine(
            string boothId,
            int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException(
                    "Quantity must be greater than zero.");
            }

            lock (DataStore.SyncRoot)
            {
                Winebooth booth =
                    DataStore.WineBooths.FirstOrDefault(
                        b => b.Id == boothId);

                if (booth == null)
                {
                    throw new ArgumentException(
                        "Booth ID not found.");
                }

                if (!booth.IsActive)
                {
                    throw new InvalidOperationException(
                        "Booth is closed.");
                }

                booth.Consume(quantity);

                Console.WriteLine(
                    $"Sold {quantity} bottles at {booth.Name} (ID: {booth.Id}).");

                Logger.Log(
                    $"Sold {quantity} bottles at booth {booth.Name}.");
            }
        }

        // Delete entity
        public static void RemoveEntity(string id)
        {
            lock (DataStore.SyncRoot)
            {
                Winebooth booth =
                    DataStore.WineBooths.FirstOrDefault(
                        b => b.Id == id);

                if (booth != null)
                {
                    DataStore.WineBooths.Remove(booth);

                    Console.WriteLine(
                        "Booth removed.");

                    Logger.Log(
                        $"Deleted booth: {booth.Name} ({booth.Id})");

                    return;
                }

                attendees attendee =
                    DataStore.Attendees.FirstOrDefault(
                        a => a.Id == id);

                if (attendee != null)
                {
                    DataStore.Attendees.Remove(
                        attendee);

                    Console.WriteLine(
                        "Attendee removed.");

                    Logger.Log(
                        $"Deleted attendee: {attendee.Name} ({attendee.Id})");

                    return;
                }

                WineStock stock =
                    DataStore.WineStocks.FirstOrDefault(
                        s => s.Id == id);

                if (stock != null)
                {
                    DataStore.WineStocks.Remove(
                        stock);

                    Console.WriteLine(
                        "Stock item removed.");

                    Logger.Log(
                        $"Deleted wine stock: {stock.Name} ({stock.Id})");

                    return;
                }
            }

            throw new ArgumentException(
                "ID not found.");
        }

        // List all entities
        public static void ListAllEntities()
        {
            lock (DataStore.SyncRoot)
            {
                Console.WriteLine(
                    "\n=== WINE BOOTHS ===");

                DataStore.WineBooths.ForEach(
                    b => Console.WriteLine(
                        b.GetStatusReport()));

                Console.WriteLine(
                    "\n=== ATTENDEES ===");

                DataStore.Attendees.ForEach(
                    a => Console.WriteLine(
                        a.GetStatusReport()));

                Console.WriteLine(
                    "\n=== WINE STOCK ===");

                DataStore.WineStocks.ForEach(
                    s => Console.WriteLine(
                        s.GetStatusReport()));
            }

            Logger.Log(
                "Displayed all festival entities.");
        }
    }
}