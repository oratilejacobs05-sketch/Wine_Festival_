using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;



namespace Wine_Festival_project
{
    public class Winebooth : Festival, IConsumables, IReport
    {
        public string VendorName { get; set; }
        public string WineVarietal { get; set; }
        public int TotalBottles { get; set; }
        public int BottlesSold { get; private set; }
        public int CurrentStock => TotalBottles - BottlesSold;

        public Winebooth(string boothName,string vendor, string varietal, int bottles) 
            : base(boothName, true)  
        {
            VendorName = vendor;
            WineVarietal = varietal;
            TotalBottles = bottles;
            BottlesSold = 0;
        }
        public void Consume(int quantity)
        {
            if (CurrentStock < quantity)
                throw new InvalidOperationException($"Not enough stock! Only {CurrentStock} bottles left.");
            BottlesSold += quantity;
        }

        public bool IsConsumed => CurrentStock <= 0;

        
        public string GetReport() =>
            $"Booth: {Name} | Vendor: {VendorName} | Varietal: {WineVarietal} | Sold: {BottlesSold}/{TotalBottles}";

        public override string GetStatusReport() =>
            $"{VendorName} ({WineVarietal}) - {CurrentStock} bottles remaining. {(IsConsumed ? "Product unaivailable (sold out)" : "Available")}";
    }
}
