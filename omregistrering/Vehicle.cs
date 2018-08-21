namespace omregistrering
{
    class Vehicle
    {
        public string RegNumber { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public double OutstandingDebt { get; set; }
        public string Basics
        {
            get
            {
                return string.Format("{0} {1}   {2}", Make, Model, RegNumber);
            }
        }

        public Vehicle(string regNumber, string make, string model, int year, double outstandingDebt)
        {
            this.RegNumber = regNumber;
            this.Make = make;
            this.Model = model;
            this.Year = year;
            this.OutstandingDebt = outstandingDebt;
        }        
    }
}
