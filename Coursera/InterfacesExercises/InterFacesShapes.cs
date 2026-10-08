namespace InheritanceApp
{
    public interface IShape
    {
        public double Area();
    }

    public interface IDrawable
    {
        void Draw();
    }
    public interface IResizable
    {
        void Resize(double factor);
    }



    public class Circle : IShape, IResizable, IDrawable
    {
        private double _radius;
        public Circle(double radius)
        {
            _radius = radius;
        }
        public double Area()
        {
            return Math.PI * _radius * _radius;
        }

        public void Draw()
        {
            Console.WriteLine($"Drawing the circle with a radius of {_radius}");
        }

        public void Resize(double newfactor)
        {
            _radius = _radius * newfactor;
        }
    }

    public class Square : IDrawable
    {
        private readonly double height, length;

        public Square(double _height, double _length)
        {
            height = _height;
            length = _length;
        }

        public double Area()
        {
            return height * length;
        }

        public void Draw()
        {
            Console.WriteLine($"Drawing the area of the sqaure with a length of {length} and a height of {height}");
        }
    }

    public class Rectangle : IShape
    {
        private readonly double width, height;

        public Rectangle(double _width, double _height)
        {
            width = _width;
            height = _height;
        }

        public double Area()
        {
            return width * height;
        }

    }

    public class Triangle : IShape
    {
        private readonly double lower, height;

        public Triangle(double _lower, double _height)
        {
            lower = _lower;
            height = _height;
        }

        public double Area()
        {
            return ((lower * height) / 2);
        }
    }

    public class Program
    {
        static void Main()
        {

            List<IShape> shapeList = new List<IShape>();
            IShape shapeinterface = new Rectangle(3, 4);
            shapeList.Add(shapeinterface);

            Circle circle = new Circle(5);
            shapeList.Add(circle);

            shapeinterface = new Triangle(2, 3);
            shapeList.Add(shapeinterface);

            foreach (var thing in shapeList)
            {
                Console.WriteLine(thing);
                Console.WriteLine(thing.Area());
            }


            List<IDrawable> drawableList = new List<IDrawable>();
            drawableList.Add(circle);

            IDrawable drawableInterface = new Square(4, 4);
            drawableList.Add(drawableInterface);
            RenderAll(drawableList);




            List<IResizable> resizeInterfaceList = new List<IResizable>();
            resizeInterfaceList.Add(circle);
            ScaleAll(resizeInterfaceList);


        }

        static void RenderAll(List<IDrawable> items)
        {
            foreach(var item in items)
            {
                item.Draw();
            }
        }
        static void ScaleAll(List<IResizable> items)
        {
            foreach(var item in items)
            {
                item.Resize(2);
            }
        }
    }
}
