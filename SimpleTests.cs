using System;

namespace Wine_Festival_project
{
    public static class SimpleTests
    {
        public static void RunTests()
        {
            Console.WriteLine("\n===== SYSTEM TESTS =====");

            TestWineBoothCreation();
            TestWineStockCreation();
            TestAttendeeCreation();

            Console.WriteLine("===== TESTS COMPLETE =====\n");
        }

        private static void TestWineBoothCreation()
        {
            Winebooth booth = new Winebooth(
                "Test Booth",
                "Test Vendor",
                "Chardonnay",
                100);

            if (booth.TotalBottles == 100)
            {
                Console.WriteLine("PASS: Wine booth creation");
            }
            else
            {
                Console.WriteLine("FAIL: Wine booth creation");
            }
        }

        private static void TestWineStockCreation()
        {
            WineStock stock = new WineStock(
                "Test Stock",
                "Cabernet",
                2020,
                200);

            if (stock.QuantityLiters == 200)
            {
                Console.WriteLine("PASS: Wine stock creation");
            }
            else
            {
                Console.WriteLine("FAIL: Wine stock creation");
            }
        }

        private static void TestAttendeeCreation()
        {
            attendees attendee = new attendees(
                "Test",
                "Person",
                "TEST01",
                25,
                false);

            if (attendee.Name == "Test Person" &&
                attendee.Age == 25)
            {
                Console.WriteLine("PASS: Attendee creation");
            }
            else
            {
                Console.WriteLine("FAIL: Attendee creation");
            }
        }
    }
}