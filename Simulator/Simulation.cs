using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator.Maps;

namespace Simulator;

public class Simulation
{
    private int _currentTurnIndex; // Current turn number - initiating with 0 -> mozna rozdzielić na Turn Index i turn number
    private int _numberOfTurns; // Number of turns based on Moves string

    public int CurrentTurnNumber
    {
        get 
        { 
            return _currentTurnIndex + 1; 
        }
    }
    
    /// <summary>
    /// Simulation's map.
    /// </summary>
    public Map Map { get; }

    /// <summary>
    /// Creatures moving on the map.
    /// </summary>
    public List<Creature> Creatures { get; }

    /// <summary>
    /// Starting positions of creatures.
    /// </summary>
    public List<Point> Positions { get; }

    /// <summary>
    /// Cyclic list of creatures moves. 
    /// Bad moves are ignored - use DirectionParser.
    /// First move is for first creature, second for second and so on.
    /// When all creatures make moves, 
    /// next move is again for first creature and so on.
    /// </summary>
    public string Moves { get; }

    /// <summary>
    /// Has all moves been done?
    /// </summary>
    public bool Finished = false;

    /// <summary>
    /// Creature which will be moving current turn.
    /// </summary>
    public Creature CurrentCreature 
    {
        get /* implement getter only */
        {
            return Creatures[_currentTurnIndex % Creatures.Count];
        }
    }


private List<Direction> _directionListForSimulation // List of parsed directions 
    {
        get
        {
            return DirectionParser.Parse(Moves);
        }
    }

    /// <summary>
    /// Lowercase name of direction which will be used in current turn.
    /// </summary> 
    public string CurrentMoveName 
    {
        get /* implement getter only */
        {
            return $"{_directionListForSimulation[_currentTurnIndex].ToString().ToLower()}";
        }
    }

    /// <summary>
    /// Simulation constructor.
    /// Throw errors:
    /// if creatures' list is empty,
    /// if number of creatures differs from 
    /// number of starting positions.
    /// </summary>
    public Simulation(Map map, List<Creature> creatures, List<Point> positions, string moves)
    { 
        Map = map;
        if (creatures.Count == 0)
        {
            throw new ArgumentException("List of Creatures cannot be empty", nameof(creatures));
        }
        else if (creatures.Count != positions.Count)
        {
            throw new ArgumentException("Number of creatures and their starting positions must match", nameof(positions));
        }
        else
        {
            Creatures = creatures;
            Positions = positions;
            Moves = moves;
            _currentTurnIndex = 0;
            _numberOfTurns = _directionListForSimulation.Count;
            // Inicjowanie stworów na mapie
            int i = 0; // Positions iterator
            foreach (Creature creature in creatures)
            {
                creature.InitMapAndPosition(Map, Positions[i]);
                i++;
            }
        }
    }

    /// <summary>
    /// Makes one move of current creature in current direction.
    /// Throw error if simulation is finished.
    /// </summary>
    public void Turn() 
    {
        CurrentCreature.Go(_directionListForSimulation[_currentTurnIndex]);
        _currentTurnIndex++; 
        if (_currentTurnIndex == _numberOfTurns)
        {
            this.Finished = true;
        }
    }
}
