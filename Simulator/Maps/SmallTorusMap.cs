using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator.Maps;

public class SmallTorusMap : Map
{
    // Właściwości
    public int Size { get; }

    public SmallTorusMap(int size)
    {
        if (size < 5 || size > 20)
        {
            throw new ArgumentOutOfRangeException(nameof(size), "For SmallTorusMap size needs to be in the range [5, 20]"); //wyjątek -> wymiar mapy nie pasuje do założeń
        }
        Size = size;
    }

    /// <summary>
    /// Check if given point belongs to the map.
    /// Map contains points from (0,0) to (Size-1, Size-1). Coordinates that equals 'Size' are outside the map!
    /// </summary>
    /// <param name="p">Point to check.</param>
    /// <returns>Bool: True/False</returns>
    public override bool Exist(Point p)
    {
        Rectangle tempRectangle = new(new Point(0, 0), new Point(Size - 1, Size - 1));
        return tempRectangle.Contains(p);
    }

    /// <summary>
    /// Next position to the point in a given direction. 
    /// </summary>
    /// <param name="p">Starting point.</param>
    /// <param name="d">Direction.</param>
    /// <returns>Next point after move. In Torus map after hitting the edge next point is on the oposite edge.</returns>
    public override Point Next(Point p, Direction d)
    {
        // brak sprawdzenia czy podany punkt jest wewnątrz mapy
        Point pointAfterMove = p.Next(d);
        if (Exist(pointAfterMove)) // To można napisać inaczej -> jak metodę NextDiagonal.. Refactoring? 
        {
            return pointAfterMove;
        }
        else
        {
            switch (d)
            {
                case Direction.Up:
                    return new Point(p.X, 0);
                case Direction.Right:
                    return new Point(0, p.Y);
                case Direction.Down:
                    return new Point(p.X, Size - 1);
                case Direction.Left:
                    return new Point(Size - 1, p.Y);    
                default:
                    return p;
            }
        }
    }

    public override Point NextDiagonal(Point p, Direction d)
    {
        // brak sprawdzenia czy podany punkt jest wewnątrz mapy
        Point pointAfterMove = p.NextDiagonal(d);
        if (Exist(pointAfterMove)) // W zasadzie wystarczyłoby to co w else... Refactoring?
        {
            return pointAfterMove;
        }
        else
        {
            switch (d)
            {
                case Direction.Up:
                    return new Point((p.X + 1) % Size, (p.Y + 1) % Size); // operator modulo 
                case Direction.Right:
                    return new Point((p.X + 1) % Size, (p.Y - 1 + Size) % Size); // + Size aby uniknąć liczb ujemnych. Dla 'modulo' nic sie nie zmienia
                case Direction.Down:
                    return new Point((p.X - 1 + Size) % Size, (p.Y - 1 + Size) % Size);
                case Direction.Left:
                    return new Point((p.X - 1 + Size) % Size, (p.Y + 1) % Size);
                default:
                    return p;
            }
        }
    }
}
