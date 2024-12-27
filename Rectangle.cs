using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator;

public class Rectangle
{
    //Pola publiczne
    public readonly int X1;
    public readonly int Y1;
    public readonly int X2;
    public readonly int Y2;

    public Rectangle(int x1,  int y1, int x2, int y2)
    {
        //sprawdzenie wspolliniowosci punktow majacych stworzać prostokat
        if (x1 == x2)
        {
            throw new ArgumentException("X1 and X2 cannot be equal. Rectangle definition requires different point coordinates in both directions.", nameof(x2));
        }
        if (y1 == y2)
        {
            throw new ArgumentException("Y1 and Y2 cannot be equal. Rectangle definition requires different point coordinates in both directions.", nameof(y2));
        }

        //Sprawdzenie kolejności argumentów - wierzchołków prostokata
        X1 = Math.Min(x1, x2); 
        Y1 = Math.Min(y1, y2);
        X2 = Math.Max(x1, x2); 
        Y2 = Math.Max(y1, y2);

    }

    public Rectangle(Point p1, Point p2) : this(p1.X, p1.Y, p2.X, p2.Y) { } 

    public bool Contains(Point point)
    {
        return X1 <= point.X && Y1 <= point.Y && X2 >= point.X && Y2 >= point.Y;
    }

    public override string ToString()
    {
        return $"({X1}, {Y1}):({X2}, {Y2})";
    }
}
