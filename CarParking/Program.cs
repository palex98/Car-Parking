using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParking
{
    class Program
    {
        static Parking parking;
        static Queue<Car> Cars;

        static void Main(string[] args)
        {
            Cars = new Queue<Car>();

            string toSplit = Console.ReadLine();
            string[] nANDm = toSplit.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            Int32.TryParse(nANDm[0], out var n);
            Int32.TryParse(nANDm[1], out var m);

            parking = new Parking(n, m);

            Console.WriteLine($"{n}, {m}");

            for(int i = 0; i<n; i++)
            {
                parking.Place[i].Price = Convert.ToInt32(Console.ReadLine()); 
            }

            for (int i = 0; i < m; i++)
            {
                Cars.Enqueue(new Car(i+1, Convert.ToInt32(Console.ReadLine())));
            }
        }
    }

    class Car
    {
        int Id { set; get; }
        int Weight { set; get; }

        public Car(int id, int weight){
            Id = id;
            Weight = weight;
        }
    }

    class ParkingPlace
    {
        bool isEmpty { set; get; }
        public int Price { set; get; }
        Car car { set; get; }

        public ParkingPlace()
        {
            isEmpty = true;
            Price = 0;
        }
    }

    class Parking
    {
        public ParkingPlace[] Place;
        
        public Parking(int n, int m)
        {
            Place = new ParkingPlace[n];
            
            for(int i=0; i<n; i++)
            {
                Place[i] = new ParkingPlace();
            }
        }
    }
}
