using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wine_Festival_project
{
    internal class Display
    {
        public delegate void SystemEventHandler(string message);


        public static event SystemEventHandler OnAlertTriggered;
        public static event SystemEventHandler OnTaskCompleted;


        public static void DisplayMenus()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("\n =========System initialized=========");
                Console.WriteLine("\n \n \n ===== Wine Country Festival Control System =====");
                Console.WriteLine("\nPlease Select An Option");
                Console.WriteLine("1. Check-in Attendee");
                Console.WriteLine("2. Register New Attendee");
                Console.WriteLine("3. Register A New Booth");
                Console.WriteLine("4. Register New Wine Stock");
                Console.WriteLine("5. Record Sold Wine");
                Console.WriteLine("6. Delete Attendee/Wine/Booth");
                Console.WriteLine("7. Show All Attendee/Wine/Booth");
                Console.WriteLine("8. Exit");
                try
                {
                    if (!int.TryParse(Console.ReadLine(), out int option))
                    {
                        throw new FormatException("Invalid selection. Please enter a valid integer choice.");
                        
                    }
                    switch (option)
                    {
                        case 1:
                            Console.WriteLine("Please Enter Attendee Ticket Number");
                            string ticket = Console.ReadLine();
                            FestivalService.CheckInAttendee(ticket);

                            break;
                        case 2:
                            

                                Console.Write("Please Enter First Name: ");
                                string fname = Console.ReadLine();

                                Console.Write("Please Enter Last Name: ");
                                string lName = Console.ReadLine();


                                Console.Write("Please Enter Age Of Attendee: ");
                                if (!int.TryParse(Console.ReadLine(), out int age) || age < 0)
                                {
                                    throw new InvalidInputFormatException("Age must be a valid positive integer.");
                                }


                                Console.Write("VIP? (true/false): ");
                                if (!bool.TryParse(Console.ReadLine(), out bool VIP))
                                {
                                    throw new InvalidInputFormatException("VIP status must be 'true' or 'false'. Entering an integer or text is invalid.");
                                }


                                int nextId = DataStore.Attendees.Count + 1;

                                string Ticket = nextId.ToString();



                                FestivalService.RegisterAttendee(fname, lName, Ticket, age, VIP);


                                OnTaskCompleted?.Invoke($"Registered Attendee: {fname} {lName} [{Ticket}]");
                            
                            

                            break;
                        case 3:
                            Console.WriteLine("Please Enter Booth Owner/Company Name");
                            string name = Console.ReadLine();
                            Console.WriteLine("Varietal Of Wines");
                            string Varietal = Console.ReadLine();
                            Console.WriteLine("Number Of Bottles Sold");
                            int amountsold = int.Parse(Console.ReadLine());

                            int index = DataStore.WineBooths.Count;
                            string boothname = "Booth " + index;
                            FestivalService.AddBooth(boothname, name, Varietal, amountsold);
                            break;
                        case 4:
                            Console.WriteLine("Please Enter Stock Name");
                            string stockName = Console.ReadLine();
                            Console.WriteLine("Please Enter Varietal Of Wine");
                            string varietal = Console.ReadLine();
                            Console.WriteLine("Please Enter Year Of Wine");
                            int year = int.Parse(Console.ReadLine());
                            Console.WriteLine("Please Enter Number Of Liters");
                            int litres = int.Parse(Console.ReadLine());
                            FestivalService.AddWineStock(stockName, varietal, year, litres);
                            break;
                        case 5:
                            Console.WriteLine("Please Enter boothID");
                            string boothID = Console.ReadLine();
                            Console.WriteLine("Please Enter Quantity");
                            int quantity = int.Parse(Console.ReadLine());
                            FestivalService.SellWine(boothID, quantity);
                            break;
                        case 6:
                            Console.WriteLine("Please Enter ID For Deleted Item/Booth/Attendee");
                            string ID = Console.ReadLine();
                            FestivalService.RemoveEntity(ID);
                            break;
                        case 7:
                            FestivalService.ListAllEntities();
                            break;
                        case 8:
                            running = false;
                            break;

                    }
                }
                catch (FormatException ex)
                {
                    OnAlertTriggered?.Invoke($"INPUT ERROR: {ex.Message}");
                }
                catch (Exception ex)
                {
                    OnAlertTriggered?.Invoke($"SYSTEM ERROR: {ex.Message}");
                }
               
            }
        }
    }
    public class InvalidInputFormatException : Exception
    {
        public InvalidInputFormatException(string message) : base(message)
        {
        }
    }

}

        




