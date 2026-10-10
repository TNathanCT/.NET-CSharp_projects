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




    public abstract class Employee
    {
        private string Name { get; set; }
        public decimal Pay { get; set; }

        public Employee(string _name)
        {
            Name = _name;
        }

        public abstract decimal CalculatePay();

        public void Describe()
        {
            Console.WriteLine($"The Employee {Name} has a salary of {Pay} per annum");
        }
    }

    public class SalariedEmployee : Employee
    {

    }










    class Program
    {
        static void Main()
        {
            Dog dog = new Dog();
            Cat cat = new Cat();

            dog.MakeSound();
            dog.Sleep();

            cat.MakeSound();
            cat.Sleep();

        }
    }
}
