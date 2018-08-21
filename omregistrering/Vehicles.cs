using System.Collections.ObjectModel;

namespace omregistrering
{

    class Vehicles:BaseViewModel
    {
        public ObservableCollection<Vehicle> Items { get; set; }

        //public static List<Vehicle> vehicles2 =
        //    new List<Vehicle>
        //{
        //    new Vehicle("BB77888", "Toyota", "Aygo", 2015, 0.0)
        //};

        public Vehicles()
        {
            this.Items = new ObservableCollection<Vehicle>
            {
                new Vehicle("XY55999", "Ford", "Ka", 2005, 0.0),
                new Vehicle("AB22333", "Renault", "Megane", 2007, 0.0),
                new Vehicle("BA50100", "VW", "Golf VI", 2013, 25000.0)
            };
        }

        public Vehicle getVehicle(string regNumber)
        {
            foreach(Vehicle item in Items)
            {
                if (item.RegNumber.Equals(regNumber))
                {
                    return item;
                }
            }
            return null;
        }
    }
}
