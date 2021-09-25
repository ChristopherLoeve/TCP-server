using System;

namespace FootballPlayerLib
{
    public class FootballPlayer
    {
        public int Id { get; set; }
        private string _name;
        private int _price;
        private int _shirtNumber;

        public string Name
        {
            get { return _name; }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentException("Name cannot be null or empty");
                if (value.Length < 4) throw new ArgumentException("Name must be at least 4 characters");
                _name = value;
            }
        }

        public int Price
        {
            get { return _price; }
            set
            {
                if (value <= 0) throw new ArgumentException("Price must be above 0");
                _price = value;
            }
        }

        public int ShirtNumber
        {
            get { return _shirtNumber; }
            set
            {
                if (value < 1 || value > 100) throw new ArgumentException("Shirt Number must be between 1 to 100");
                _shirtNumber = value;
            }
        }

        public FootballPlayer(int id, string name, int price, int shirtNumber)
        {
            Id = id;
            Name = name;
            Price = price;
            ShirtNumber = shirtNumber;
        }

        public override string ToString()
        {
            return $"{Id}: Player {Name} with shirt number {ShirtNumber}. Price: {Price}";
        }
    }
}
