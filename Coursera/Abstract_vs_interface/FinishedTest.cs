namespace InheritanceApp
{
    public abstract class Animal
    {
        public abstract void MakeSound();
        public void Sleep()
        {
            Console.WriteLine("Sleeping");
        }
    }

    public class Dog : Animal
    {
        public override void MakeSound()
        {
            Console.WriteLine("Woof");
        }
    }

    public class Cat : Animal
    {
        public override void MakeSound()
        {
            Console.WriteLine("Meow");
        }
    }



    public interface IPayable
    {
        public decimal CalculatePay();
    }

    public abstract class Employee : IPayable
    {
        public string Name { get; set; }
        public decimal Pay { get; set; }

        public Employee(string _name)
        {
            Name = _name;
        }

        public abstract decimal CalculatePay();

        public void Describe()
        {
            Console.WriteLine($"The Employee {Name} has a salary of {CalculatePay()} per annum");
        }
    }

    public class SalariedEmployee : Employee
    {
        public decimal FixedAnnualAmount { get; set; }

        public SalariedEmployee(string name, decimal fixedannualamount) : base(name)
        {
            FixedAnnualAmount = fixedannualamount;
        }
        public override decimal CalculatePay()
        {
            return FixedAnnualAmount / 12;
        }
    }

    public class HourlyEmployee : Employee
    {
        public decimal rateperhour { get; set; }

        public float hourshired { get; set; }

        public HourlyEmployee(string name, decimal rate, float hours) : base(name)
        {
            rateperhour = rate;
            hourshired = hours;
        }

        public override decimal CalculatePay()
        {
            return rateperhour * (decimal)hourshired;
        }

    }


    public class Contractor : IPayable
    {
        public decimal Payment { get; set; }

        public decimal CalculatePay()
        {
            return Payment;
        }

        public Contractor(decimal _salary)
        {
            Payment = _salary;
        }
    }










    class Program
    {
        static void Main()
        {

            List<Employee> employees = new List<Employee>();

            SalariedEmployee Jon = new SalariedEmployee("Jon", 1200);
            HourlyEmployee Kate = new HourlyEmployee("Kate", 12, 1000);

            employees.Add(Jon);
            employees.Add(Kate);

            for (int i = 0; i < employees.Count; i++)
            {
                employees[i].Describe();
            }


            List<IPayable> fullemployeesList = new List<IPayable>();
            IPayable contractor = new Contractor(2200);
            fullemployeesList.Add(contractor);
            fullemployeesList.Add(Kate);
            fullemployeesList.Add(Jon);
            Console.WriteLine($"The overall salary average is : {TotalPayroll(fullemployeesList)}");





            Dog dog = new Dog();
            Cat cat = new Cat();

            dog.MakeSound();
            dog.Sleep();

            cat.MakeSound();
            cat.Sleep();

        }


        static decimal TotalPayroll(List<IPayable> people)
        {
            decimal overall = 0;
            for (int i = 0; i < people.Count; i++)
            {
                overall += people[i].CalculatePay();
            }

            overall = overall / people.Count;

            return overall;
        }
    }
}
