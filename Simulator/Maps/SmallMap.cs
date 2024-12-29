using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator.Maps;

public abstract class SmallMap : Map
{
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
        SizeX = sizeX;
        SizeY = sizeY;
    }
}
