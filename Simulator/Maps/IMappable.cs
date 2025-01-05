using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator.Maps;

public interface IMappable
{

    char MapSymbol { get; }

    public Map? Map { get; set; }

    public Point Position { get; set; }


    string Go(Direction direction); 

    void InitMapAndPosition(Map map, Point position, bool requestFromMap = false);

    void RemoveFromMap();

}
