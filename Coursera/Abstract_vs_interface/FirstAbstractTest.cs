namespace InheritanceApp
{
    public abstract class PrinterBase
    {
        public abstract void Print(string document);      // no body — children must write it

        public void PowerOn()                             // has a body — children get it free
        {
            Console.WriteLine("Powering on...");
        }
    }


    public class LaserPrinter : PrinterBase
    {
        public override void Print(string document)
        {
            Console.WriteLine($"Laser printing: {document}");
        }
    }


    public class InkjetPrinter : PrinterBase
    {
        public override void Print(string document)
        {
            Console.WriteLine($"Inkjet printing: {document}");
        }
    }

    class Program
    {
        static void Main()
        {
            var laser = new LaserPrinter();
            laser.PowerOn();            // came from the base — never written in LaserPrinter
            laser.Print("report.pdf");  // LaserPrinter's own version

            var inkjet = new InkjetPrinter();
            inkjet.PowerOn();           // same base code again
            inkjet.Print("photo.jpg");  // InkjetPrinter's own version
        }
    }
}
