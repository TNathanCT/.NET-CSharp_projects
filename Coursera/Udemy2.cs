namespace InheritanceApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Collie mydog = new Collie();
            mydog.Eat();
            mydog.Bark();
            mydog.Running();


        }
    }

    class Animal
    {
        public void Eat()
        {
            Console.WriteLine("Eating");
        }
    }

    class Dog : Animal
    {
        public void Bark()
        {
            Console.WriteLine("Woof");
        }
    }


    class Collie : Dog
    {
        public void Running()
        {
            Console.WriteLine("Zoomies");
        }
    }

}
