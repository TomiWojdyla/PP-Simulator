using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator.Maps;

public interface IMappable
{

    public bool IsLost { get; set; } 

    char MapSymbol { get; }

    public Map? Map { get; set; }

    public Point Position { get; set; }


    string Go(Direction direction); 

    void InitMapAndPosition(Map map, Point position, bool requestFromMap = false);

    void RemoveFromMap();

    public void RandomMove()
    {
        Random random = new Random();
        Array allMoves = Enum.GetValues(typeof(Direction));
        Direction randomMove = (Direction)allMoves.GetValue(random.Next(allMoves.Length));
        this.Go(randomMove);
    }

}
