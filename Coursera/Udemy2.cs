namespace InheritanceApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee newEmployee = new Employee("Joe", 26);
            newEmployee.DisplayPersonDetails();
        }
    }


    public class Person
    {
        public string Name { get; private set; }
        public int Age { get; private set; }

        public Person(string _name, int _age)
        {
            Name = _name;
            Age = _age;
            Console.WriteLine("This is a constructor");
        }
        public void DisplayPersonDetails()
        {
            Console.WriteLine($"Name : {Name}, Age: {Age}.");
        }
    }

    public class Employee : Person
    {
        public Employee(string name, int age) : base(name, age)
        {

        }
    }
}
