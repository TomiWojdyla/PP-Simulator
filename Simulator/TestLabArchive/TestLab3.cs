using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator.TestLabArchive
{
    public class TestLab3
    {
        public static void Lab3b()
        {
            Elf c = new("Shrek", 7);
            Console.WriteLine(c.Greeting());

            Console.WriteLine("\n* Up");
            Console.WriteLine(c.Go(Direction.Up));

            Console.WriteLine("\n* Right, Left, Left, Down");
            Direction[] directions = {
            Direction.Right, Direction.Left, Direction.Left, Direction.Down
            };
            string[] goTable = c.Go(directions);
            foreach (string go in goTable)
            {
                Console.WriteLine(go);
            }

            Console.WriteLine("\n* LRL");
            string[] goTable2 = c.Go("LRL");
            foreach (string go in goTable2)
            {
                Console.WriteLine(go);
            }

            Console.WriteLine("\n* xxxdR lyyLTyu");
            string[] goTable3 = c.Go("xxxdR lyyLTyu");
            foreach (string go in goTable3)
            {
                Console.WriteLine(go);
            }
        }
    }
}
