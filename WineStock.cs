using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wine_Festival_project
{
    public class WineStock : Festival ,IReport
    {
        public string WineName { get; set; }
        public int VintageYear { get; set; }
        public int QuantityLiters { get; set; }

        public WineStock(string stockName, string Wname, int year, int liters)
            : base(stockName, true)  
        {
            WineName = Wname;
            VintageYear = year;
            QuantityLiters = liters;
        }

        public string GetReport() =>
           $"{WineName} {VintageYear} | {QuantityLiters}L in storage";

        public override string GetStatusReport() =>
            $"{WineName} ({VintageYear}) - {QuantityLiters}L available.";
    }

}

