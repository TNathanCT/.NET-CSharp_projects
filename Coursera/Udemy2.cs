namespace InheritanceApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee newEmployee = new Employee("Joe", 26, "Sales Rep", 12345);
            newEmployee.DisplayEmployeeInfo();
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
        public string JobTitle { get; private set; }
        public int EmployeeID{ get; private set; }

        public Employee(string name, int age, string jobtitle, int id) : base(name, age)
        {
            JobTitle = jobtitle;
            EmployeeID = id;
        }


        public void DisplayEmployeeInfo()
        {
            DisplayPersonDetails();
            Console.WriteLine($"The Employee with the ID : {EmployeeID} works as a {JobTitle}.");
        }
    }
}
