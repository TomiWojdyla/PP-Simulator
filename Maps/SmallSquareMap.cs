using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator.Maps;

/// <summary>
/// Small Square Map of points ( 5 < size < 20).
/// </summary>
internal class SmallSquareMap : Map
{
    //Właściwości
    public int Size { get; }

    public SmallSquareMap(int size)
    {
        if (size < 5 || size > 20)
        {
            throw new ArgumentOutOfRangeException(nameof(size), "For SmallSquareMap size needs to be in the range [5, 20]");
        }
        Size = size;
        
    }
    public override bool Exist(Point p)
    {
        return 0 <= p.X && p.X <= Size-1 && 0 <p.Y && p.Y <= Size-1;
    }

    public override Point Next(Point p, Direction d)
    {
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

    public override Point NextDiagonal(Point p, Direction d)
    {
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