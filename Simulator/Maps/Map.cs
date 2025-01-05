using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator.Maps;

/// <summary>
/// Map of points.
/// </summary>
public abstract class Map 
{
    /// <summary>
    /// Horizontal map size.
    /// </summary>
    public int SizeX { get; init; }

    /// <summary>
    /// Vertical map size.
    /// </summary>
    public int SizeY { get; init; }

    private readonly Rectangle _map;

    public Map(int sizeX, int sizeY)
    {
        if (sizeX < 5)
        {
            throw new ArgumentOutOfRangeException(nameof(sizeX), "Map size cannot be smaller than 5"); //wyjątek -> wymiar mapy nie pasuje do założeń
        }
        else if (sizeY < 5)
        {
            throw new ArgumentOutOfRangeException(nameof(sizeY), "Map size cannot be smaller than 5"); //wyjątek -> wymiar mapy nie pasuje do założeń
        }
        SizeX = sizeX;
        SizeY = sizeY;
        _map = new Rectangle(0, 0, SizeX - 1, SizeY - 1);
    }

    /// <summary>
    /// Check if given point belongs to the map.
    /// </summary>
    /// <param name="p">Point to check.</param>
    /// <returns></returns>
    public bool Exist(Point p)
    {
        return _map.Contains(p);
    }

    /// <summary>
    /// Next position to the point in a given direction.
    /// </summary>
    /// <param name="p">Starting point.</param>
    /// <param name="d">Direction.</param>
    /// <returns>Next point.</returns>
    public abstract Point Next(Point p, Direction d);

    /// <summary>
    /// Next diagonal position to the point in a given direction 
    /// rotated 45 degrees clockwise.
    /// </summary>
    /// <param name="p">Starting point.</param>
    /// <param name="d">Direction.</param>
    /// <returns>Next point.</returns>
    public abstract Point NextDiagonal(Point p, Direction d);

    public abstract void Add(IMappable mappable, Point point);


    public abstract void Remove(IMappable mappable, Point point);
 

    public void Move(IMappable mappable, Point startPoint, Point endPoint)
    {
        Remove(mappable, startPoint); //jezeli stwora nie ma w tym punkcie wystopowac Adda
        Add(mappable, endPoint);
        //return $"Przesunalem {creature} stad {startPoint} tutaj {endPoint}";
    }

    public abstract List<IMappable> At(int x, int y); 

    public abstract List<IMappable> At(Point point);
}
