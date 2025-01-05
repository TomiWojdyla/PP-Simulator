using System.Diagnostics.Tracing;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using Simulator;
using Simulator.Maps;
using static SimConsole.MapVisualizer;


namespace SimConsole;

internal class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("SIMULATION!\n");

        SmallTorusMap map = new(8, 6);
        List<IMappable> creatures = [new Orc("Gorbag"), new Elf("Elandor"),
            new Animals() { Description = "Rabbits" }, new Birds() { Description = "Eagles"}, new Birds() {Description = "Ostriches", CanFly = false}];
        List<Simulator.Point> points = [new(2, 2), new(3, 1), new(5, 5), new(7, 3), new(0, 4)];
        string moves = "dlrludluddlrulr";

        Simulation simulation = new(map, creatures, points, moves);
        MapVisulizer mapVisualizer = new(simulation.Map);



        //SmallSquareMap map = new(5);
        //List<IMappable> creatures = [new Orc("Gorbag"), new Elf("Elandor")];
        //List<Simulator.Point> points = [new(2, 2), new(3, 1)];
        //string moves = "dlrludl";

        //Simulation simulation = new(map, creatures, points, moves);
        //MapVisulizer mapVisualizer = new(simulation.Map);


        Console.WriteLine("Created Creatures in map:");
        foreach (IMappable creature in creatures)
        {
            Console.WriteLine(creature.ToString());
        }
        Console.WriteLine("\nStarting Positions:");


        mapVisualizer.Draw();
        while (!simulation.Finished)
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.WriteLine($"\nTurn {simulation.CurrentTurnNumber}:");
            Console.Write(simulation.CurrentCreature);
            Console.Write(" goes ");
            Console.Write(simulation.CurrentMoveName);
            Console.WriteLine(":");
            simulation.Turn();
            mapVisualizer.Draw();
        }
        Console.WriteLine("End of simulation!");


        //SmallSquareMap map = new(5);
        //List<Creature> creatures = [new Orc("Gorbag"), new Elf("Elandor")];
        //List<Simulator.Point> points = [new(2, 2), new(3, 1)];
        //string moves = "dlrludl";

        //Simulation simulation = new(map, creatures, points, moves);
        //foreach (Creature creature in map.At(2, 2))
        //{
        //    Console.WriteLine("Creatures in (2,2)");
        //    Console.WriteLine(creature.Name);
        //}
        //foreach (Creature creature in map.At(3, 1))
        //{
        //    Console.WriteLine("Creatures in (3,1)");
        //    Console.WriteLine(creature.Name);
        //}
        //simulation.Turn();
        //Console.WriteLine(map.At(2, 2));
        //Console.WriteLine(map.At(2, 1));
        //Console.WriteLine(map.At(3, 1));
        //simulation.Turn();
        //foreach (Creature creature in map.At(2, 1))
        //{
        //    Console.WriteLine("Creatures in (2,1)");
        //    Console.WriteLine(creature.Name);
        //}
        //foreach (Creature creature in map.At(3, 1))
        //{
        //    Console.WriteLine("Creatures in (3,1)");
        //    Console.WriteLine(creature.Name);
        //}



    }
}
