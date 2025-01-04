using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator.Maps
{
    public class SmallSquareMap : SmallMap
    {
        //Właściwości
        public int Size { get; }

        public SmallSquareMap(int size) : base(size, size)
        {
        }

        /// <summary>
        /// Next position to the point in a given direction.
        /// </summary>
        /// <param name="p">Starting point.</param>
        /// <param name="d">Direction.</param>
        /// <returns>Next point after move or input point if given move takes it outside the map.</returns>
        public override Point Next(Point p, Direction d)
        {
            // brak sprawdzenia czy podany punkt jest wewnątrz mapy
            Point pointAfterMove = p.Next(d);
            if (Exist(pointAfterMove))
            {
                return pointAfterMove;
            }
            else
            {
                return p;
            }
        }

        /// <summary>
        /// Next diagonal position to the point in a given direction 
        /// rotated 45 degrees clockwise.
        /// </summary>
        /// <param name="p">Starting point.</param>
        /// <param name="d">Direction.</param>
        /// <returns>Next point after move or input point if given move takes it outside the map.</returns>
        public override Point NextDiagonal(Point p, Direction d)
        {
            // brak sprawdzenia czy podany punkt jest wewnątrz mapy
            Point pointAfterMove = p.NextDiagonal(d);
            if (Exist(pointAfterMove))
            {
                return pointAfterMove;
            }
            else
            {
                return p;
            }
        }
    }
}
