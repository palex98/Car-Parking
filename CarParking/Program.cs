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
        static List<Car> Cars;
        static Queue<Car> queue;
        public static long money = 0;

        static void Main(string[] args)
        {
            Cars = new List<Car>();
            queue = new Queue<Car>();

            string toSplit = Console.ReadLine();
            string[] nANDm = toSplit.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            Int32.TryParse(nANDm[0], out var n);
            Int32.TryParse(nANDm[1], out var m);

            parking = new Parking(n, m);

            for (int i = 0; i < n; i++)
            {
                parking.Place[i].Price = Convert.ToInt32(Console.ReadLine());
            }

            for (int i = 0; i < m; i++)
            {
                Cars.Add(new Car(i + 1, Convert.ToInt32(Console.ReadLine())));
            }

            for (int i = 0; i < 2 * m; i++)
            {
                int number = Convert.ToInt32(Console.ReadLine());
                if (number > 0)
                {
                    if (!parking.CarIn(Cars[number - 1]))
                    {
                        queue.Enqueue(Cars[number - 1]);
                    }
                }
                else if (number < 0)
                {
                    parking.CarOut(number);

                    if (queue.Count != 0)
                    {
                        parking.CarIn(queue.Dequeue());
                    }
                }
            }
            Console.WriteLine(money);
            Console.ReadLine();
        }
    }

    class Car
    {
        public int Id { set; get; }
        public int Weight { set; get; }

        public Car(int id, int weight)
        {
            Id = id;
            Weight = weight;
        }
    }

    class ParkingPlace
    {
        public bool isEmpty { set; get; }
        public int Price { set; get; }
        public Car car { set; get; }

        public ParkingPlace()
        {
            isEmpty = true;
            Price = 0;
        }

        public void Clearing()
        {
            isEmpty = true;
            car = null;
        }
    }

    class Parking
    {
        public ParkingPlace[] Place;

        public Parking(int n, int m)
        {
            Place = new ParkingPlace[n];

            for (int i = 0; i < n; i++)
            {
                Place[i] = new ParkingPlace();
            }
        }

        public bool CarIn(Car car)
        {
            foreach (var place in Place)
            {
                if (place.isEmpty)
                {
                    place.car = car;
                    place.isEmpty = false;
                    Program.money += MoneyCalculator(car, place);
                    return true;
                }
            }
            return false;
        }

        public void CarOut(int id)
        {
            foreach (var place in Place)
            {
                if (place.isEmpty) continue;
                if (place.car.Id == Math.Abs(id))
                {
                    place.Clearing();
                }
            }
        }

        public long MoneyCalculator(Car car, ParkingPlace place)
        {
            return car.Weight * place.Price;
        }
    }
}
