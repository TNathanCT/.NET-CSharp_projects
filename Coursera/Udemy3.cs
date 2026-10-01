namespace InheritanceApp
{


    public interface IAnimal
    {
        void MakeSound();
        void Eat(string food);
    }

    public class Dog : IAnimal
    {
        public void Eat(string food)
        {
            Console.WriteLine($"Dog eats : {food}");
        }

        public void MakeSound()
        {
            Console.WriteLine("Dog Barks.");
        }
    }

    public class Cat : IAnimal
    {
         public void Eat(string food)
        {
            Console.WriteLine($"Cat eats : {food}");
        }

        public void MakeSound()
        {
            Console.WriteLine("Cat Meows.");
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Dog dog = new Dog();
            dog.MaskeSound();
            dog.Eat("Treat");



            Cat cat = new Cat();
            cat.MaskeSound();
            cat.Eat("Mouse");
        }
    }


}
