using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Wine_Festival_project
{
    
        public abstract class Festival
        {
            private static int _globalCounter = 0;  // global counter for all entities

            public string Id { get; protected set; }
            public string Name { get; set; }
            public bool IsActive { get; set; }

            
            public Festival(string firstName, string surname)
            {
                _globalCounter++;
            //gets first two letters of name and surname
                string firstNamePrefix = firstName.Length >= 2
                    ? firstName.Substring(0, 2).ToUpper()
                    : firstName.ToUpper().PadRight(2, 'X');//Adds X if name is only one letter

                string surnamePrefix = surname.Length >= 2
                    ? surname.Substring(0, 2).ToUpper()
                    : surname.ToUpper().PadRight(2, 'X');

            //adding unique identifier
                string randomPart = Guid.NewGuid().ToString().Substring(0, 2).ToUpper();

            //creating ID
                Id = $"{firstNamePrefix}-{surnamePrefix}-{randomPart}{_globalCounter:D2}";
                Name = $"{firstName} {surname}";
                IsActive = true;
            }

            // Constructor for entities without surname (Booths, Stock)
            public Festival(string name, bool useNameOnly = true)
            {
                _globalCounter++;

               //Booth names using prefixes
                string namePrefix = name.Length >= 3
                    ? name.Substring(0, 3).ToUpper()
                    : name.ToUpper().PadRight(3, 'X');

                string randomPart = Guid.NewGuid().ToString().Substring(0, 2).ToUpper();

                Id = $"{namePrefix}-{randomPart}{_globalCounter:D2}";
                Name = name;
                IsActive = true;
            }

            public abstract string GetStatusReport();

        //Checks booth activity status
        public override string ToString()
            {
                return $"[{Id}] {Name} - {(IsActive ? "Active" : "Inactive")}";
            }
        }
    }








