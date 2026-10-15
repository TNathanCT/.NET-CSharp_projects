using System.Diagnostics;
using System;

namespace InheritanceApp
{
    public enum ParcelSize{small, medium, large};
      public interface ITrackable
        {
            public string GetTrackingNumber();
        }

    public struct Address
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string Postcode { get; set; }

        public Address(string _city, string _street, string _postode)
        {
            City = _city;
            Street = _street;
            Postcode = _postode;
        }

        public string Format()
        {
            return "Lives as : " + City + " " + Street + " " + Postcode;
        }
    }

    public abstract class Parcel : ITrackable
    {
        private static readonly Random randnum = new Random();
        private readonly string _trackingNumber;
        protected Address Destination;
        protected ParcelSize Size;
        public decimal Weight { get; set; }

        public Parcel(Address _destination, ParcelSize _size, decimal _weight)
        {
            if(Weight <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(Weight), "Weight must be greater than zero");
            }
                Destination = _destination;
                Size = _size;
                Weight = _weight;
                _trackingNumber = "PCL-" + randnum.Next();



        }

        public abstract decimal CalculateCost();

        public void Describe()
        {
            Console.WriteLine($"It weights {Weight}. And the cost is :{CalculateCost()}");
        }

        public string GetTrackingNumber()
        {
            return _trackingNumber;
        }

    }

    public class StandardParcel : Parcel
    {
        public decimal Cost { get; set; }

        public StandardParcel(decimal _cost, decimal _weight, Address _address, ParcelSize _size) : base(_address, _size, _weight)
        {
            Cost = _cost;
        }
        public override decimal CalculateCost()
        {
            if (Size == ParcelSize.large)
            {
                return (Weight * 2) + 5;
            }
            else
            {
                return (Weight * 2);
            }
        }
    }


    public class ExpressParcel : Parcel
    {
        public decimal Cost { get; set; }
        public decimal PriorityFee { get; set; }
        public ExpressParcel(decimal _priorityfee, decimal _cost, decimal _weight, Address _address, ParcelSize _size) : base(_address, _size, _weight)
        {
            PriorityFee = _priorityfee;
            Cost = _cost;
        }

        public override decimal CalculateCost()
        {
            return (Weight * 5) + PriorityFee;
        }

        public virtual int EstimatedDays()
        {
            return 1;
        }

    }



    public class Courier : ITrackable
    {
        public string TrackingNumberVan{ get; set; }

        public Courier(string _number)
        {
            TrackingNumberVan = _number;
        }
        public string GetTrackingNumber()
        {
            return TrackingNumberVan;
        }
    }

    class Program
    {




        static void Main()
        {
            Address add = new Address("London", "Tunis Road", "W127EZ");


            List<ITrackable> listofIparcels = new List<ITrackable>();
            ITrackable standardParcel = new StandardParcel((decimal)0.1, 2, add, ParcelSize.medium);


            add = new Address("Paris", "Neuilly Sur Seine", "99200");
            ITrackable expressparcel = new ExpressParcel((decimal)3, (decimal)2, 12, add, ParcelSize.medium);

            listofIparcels.Add(standardParcel);
            listofIparcels.Add(expressparcel);


            List<Parcel> listofparcels = new List<Parcel>();
            StandardParcel standardParcelP = new StandardParcel((decimal)0.1, 2, add, ParcelSize.medium);
            ExpressParcel expressparcelP = new ExpressParcel((decimal)3, (decimal)2, 12, add, ParcelSize.medium);
            var parcels = new List<Parcel> { standardParcelP, expressparcelP };
            foreach (Parcel? p in parcels) p.Describe();

            try
            {
                StandardParcel parcel = new StandardParcel(12, 0, add, ParcelSize.large);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex);
            }


            listofIparcels.Add(new Courier("VAN-4471"));
            TrackAll(listofIparcels);





        }

        public int FindTrackingNumber(string trackingnumber)
        {
            int result;
            trackingnumber = trackingnumber.Remove(0, 3);
            bool success;
            try {
                return int.Parse(trackingnumber);
            }
            catch (FormatException ex) {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }


        static void TrackAll(List<ITrackable> items)
        {

            for(int i = 0; i<items.Count; i++)
            {
                Console.WriteLine($"The tracking number is : {items[i].GetTrackingNumber()}");
            }

        }

    }
}
