namespace InheritanceApp
{
    public interface ISign
    {
        public void CardPayment();
    }

    public class LeftShop : ISign
    {
        public void CardPayment()
        {
            Console.WriteLine("We accept card payment");
        }

        public void CashPayment()
        {
            Console.WriteLine("We also accept cash");
        }

        public void Bread()
        {
            Console.WriteLine("This is bread");
        }
    }

    public class RightShop
    {
        public void CashPayment()
        {
            Console.WriteLine("We only accept cash");
        }

        public void Bread()
        {
            Console.WriteLine("This is bread");
        }
    }


    public class WeWantToGoShopping
    {
        private readonly ISign signinterface;

        public WeWantToGoShopping(ISign sign)
        {
            signinterface = sign;
        }

        public void WeCanGoShoppingHere()
        {
            signinterface.CardPayment();
        }
    }




    class Program
    {
        static void Main(string[] args)
        {

            //The first is the one that matters in real code. 
            // WeWantToGoShopping works with any shop that has the sign, 
            // and knows nothing about LeftShop.
            ISign isign = new LeftShop();
            WeWantToGoShopping task = new WeWantToGoShopping(isign);
            task.WeCanGoShoppingHere();


            isign.CardPayment();

        }
    }


}
