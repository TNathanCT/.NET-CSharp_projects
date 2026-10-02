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



namespace InheritanceApp
{

    public interface iPaymentProcessor
    {
        void ProcessPayment(decimal amount);
    }




    public class CreditCardProcessor : iPaymentProcessor
    {
        public void ProcessPayment(decimal amout)
        {
            Console.WriteLine($"Procession credit card of {amount}");
        }
    }


    public class PaypalProcessor : iPaymentProcessor
    {
        public void ProcessPayment(decimal amout)
        {
            Console.WriteLine($"Procession paypal of {amount}");
        }

    }





    public class PaymentService
    {
        private readonly iPaymentProcessor _processor;

        public PaymentService(iPaymentProcessor processor)
        {
            _processor = processor;
        }

        public void ProcessOrderPayment(decimal amount)
        {
            _processor.ProcessPayment(amount);
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            iPaymentProcessor creditcardprocessor = new CreditCardProcessor();
            PaymentService paymentService = new PaymentService(creditcardprocessor);
            paymentService.ProcessOrderPayment(100.00m);


            iPaymentProcessor paypalprocessor = new PaypalProcessor();
            paypalprocessor = new PaypalProcessor();
            paypalprocessor.ProcessOrderPayment(200.00m);
        }
    }


}
