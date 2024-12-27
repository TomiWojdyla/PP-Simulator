using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using Simulator.Maps;

namespace Simulator;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting Simulator!\n");
        Lab5b();
    }

    public static void Lab5b()
    {
        SmallSquareMap smallMap1 = new(13); // no exception
        Console.WriteLine($"Small map created. Size {smallMap1.Size}.");

        Point testPoint1 = new(1, 1); // punkt wewnątrz mapy
        Console.WriteLine(testPoint1);
        Console.WriteLine($"Test Point 1 exist in the map? {smallMap1.Exist(testPoint1)}.");
        Point movedTestPoint1 = smallMap1.Next(testPoint1, Direction.Right);
        Console.WriteLine(movedTestPoint1);
        Console.WriteLine($"Test Point 1 exist in the map? {smallMap1.Exist(movedTestPoint1)}.");
        movedTestPoint1 = smallMap1.NextDiagonal(movedTestPoint1, Direction.Up);
        Console.WriteLine(movedTestPoint1);
        Console.WriteLine($"Test Point 1 exist in the map? {smallMap1.Exist(movedTestPoint1)}.");

        Point testPoint2 = new(12, 12); //punkt wejsciowy na krawedzi mapy
        Console.WriteLine(testPoint2);
        Console.WriteLine($"Test Point 2 exist in the map? {smallMap1.Exist(testPoint2)}.");
        Point movedTestPoint2 = smallMap1.NextDiagonal(testPoint2, Direction.Up);
        Console.WriteLine(movedTestPoint2);
        Console.WriteLine($"Test Point 2 exist in the map? {smallMap1.Exist(movedTestPoint2)}.");

        Point testPoint3 = new(-2, 10); //Punkt wejsciowy poza mapą
        Console.WriteLine(testPoint3);
        Console.WriteLine($"Test Point 3 exist in the map? {smallMap1.Exist(testPoint3)}.");
        Point movedTestPoint3 = smallMap1.NextDiagonal(testPoint3, Direction.Left);
        Console.WriteLine(movedTestPoint3);
        Console.WriteLine($"Test Point 3 exist in the map? {smallMap1.Exist(movedTestPoint3)}.");


        try
        {
            SmallSquareMap smallMap2 = new(7); //no exception
            Console.WriteLine($"Small map created. Size {smallMap2.Size}.");

            SmallSquareMap smallMap3 = new(4); //exception expected -> too small
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            try
            {
                SmallSquareMap smallMap4 = new(33); //exception expected -> too big
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



        /*
        Expected output:
        Small mape created. Size 13.
        (1, 1)
        Test Point 1 exist in the map? True.
        (2, 1)
        Test Point 1 exist in the map? True.
        (3, 2)
        Test Point 1 exist in the map? True.
        (12, 12)
        Test Point 2 exist in the map? True.
        (12, 12)
        Test Point 2 exist in the map? True.
        (-2, 10)
        Test Point 3 exist in the map? False.
        (-2, 10)
        Test Point 3 exist in the map? False.
        Small mape created. Size 7.
        For SmallSquareMap size needs to be in the range [5, 20]. (Parameter 'Size')
        For SmallSquareMap size needs to be in the range [5, 20]. (Parameter 'Size')
        */
    }
}
