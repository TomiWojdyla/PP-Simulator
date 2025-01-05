using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator.Maps;

public class SmallTorusMap : SmallMap
{
    // Właściwości
    public int SizeX { get; }

    public int SizeY { get; }

    public SmallTorusMap(int sizeX, int sizeY) : base(sizeX, sizeY)
    {
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
                    return new Point(p.X, SizeY - 1);
                case Direction.Left:
                    return new Point(SizeX - 1, p.Y);    
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
                    return new Point((p.X + 1) % SizeY, (p.Y + 1) % SizeY); // operator modulo 
                case Direction.Right:
                    return new Point((p.X + 1) % SizeX, (p.Y - 1 + SizeX) % SizeX); // + Size aby uniknąć liczb ujemnych. Dla 'modulo' nic sie nie zmienia
                case Direction.Down:
                    return new Point((p.X - 1 + SizeY) % SizeY, (p.Y - 1 + SizeY) % SizeY);
                case Direction.Left:
                    return new Point((p.X - 1 + SizeX) % SizeX, (p.Y + 1) % SizeX);
                default:
                    return p;
            }
        }
    }

    //private Point ToTorus(Point point)
    //{
    //    var x = point.X;
    //    while (x >= SizeX) x -= SizeX;
    //    while (x <= 0) x += SizeX;

    //    var y = point.Y;
    //    while (y >= SizeY) y -= SizeY;
    //    while (y <= 0) y += SizeY;

    //    return new Point(x, y);
    //}
}
