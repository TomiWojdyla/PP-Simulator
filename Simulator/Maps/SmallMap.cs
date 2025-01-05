using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Simulator.Maps;

public abstract class SmallMap : Map
{
    public List<IMappable>?[,] _fields;

    public SmallMap(int sizeX, int sizeY) : base(sizeX, sizeY)
    {
        if (sizeX > 20)
        {
            throw new ArgumentOutOfRangeException(nameof(sizeX), "Map size cannot be larger than 20"); //wyjątek -> wymiar mapy nie pasuje do założeń
        }
        else if (sizeY > 20)
        {
            throw new ArgumentOutOfRangeException(nameof(sizeY), "Map size cannot be larger than 20"); //wyjątek -> wymiar mapy nie pasuje do założeń
        }

        _fields = new List<IMappable>?[sizeX, sizeY];
    }

    public override void Add(IMappable mappable, Point point)
    {
        if (_fields[point.X, point.Y] != null && _fields[point.X,point.Y].Count != 0)
        {
            _fields[point.X, point.Y].Add(mappable);
        }
        else 
        {
            var lista = new List<IMappable> { mappable };
            _fields[point.X, point.Y] = lista;
        }
        mappable.InitMapAndPosition(this, point, true);
        //Console.WriteLine($"ADD Function Dodałem {creature} w punkcie {point}");
    }

    public override void Remove(IMappable mappable, Point point)
    {
        //dodac sprawdzenie czy stwór jest w tym punkcie
        _fields[point.X,point.Y].Remove(mappable);
        mappable.RemoveFromMap();
        //Console.WriteLine($"Remove Function: Usunąłem {creature} z punktu {point}");    
    }

    public override List<IMappable> At(int x, int y)
    {
        var point = new Point(x, y);
        return this.At(point);
    }

    public override List<IMappable> At(Point point)
    {
        if (this.Exist(point) == false)
        {
            //return $"Point {point} does not belong to the map";
            return new List<IMappable> { };
        }
        else
        {
            var listOfCreaturesInPoint = _fields[point.X, point.Y];
            if (listOfCreaturesInPoint != null && listOfCreaturesInPoint.Count != 0)
            {
                var listaStworow = new String("");
                foreach (Creature creature in listOfCreaturesInPoint)
                {
                    var creatureName = creature.Name;
                    listaStworow += creatureName + ", ";
                }
                //return $"The creatures in point {point} are as follows: {listaStworow}";
                return listOfCreaturesInPoint;
            }
            else
            {
                //return $"In the indicated Point there are no creatures";
                return new List<IMappable> { };
            }
        }
    }
}
