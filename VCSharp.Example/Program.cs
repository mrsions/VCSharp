namespace VCSharp.Example
{
    public class Program
    {
        static void Main(string[] args)
        {
            ObjPtr o = new ObjPtr(new List<object?> { 0, 1, 2, 3, 4, 5 }, 0);

            Console.WriteLine((++o).Point);
            Console.WriteLine((++o).Point);
            Console.WriteLine((++o).Point);
            Console.WriteLine((++o).Point);
            Console.WriteLine((++o).Point);
            Console.WriteLine((++o).Point);
            Console.WriteLine((++o).Point);


        }
    }
}