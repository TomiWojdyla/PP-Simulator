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
    private int _currentCreatureIndex; 
    private int _numberOfTurns; // Number of turns based on Moves string
    private bool _creatureKilled = false;
    public bool simulationMessage = false;
    public string endingMessage;

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
    public List<IMappable> Creatures { get; }

    /// <summary>
    /// Starting positions of creatures.
    /// </summary>
    public List<Point> Positions { get; }

    /// <summary>
    /// Creatures moving on the map.
    /// </summary>
    public List<StaticObstacle> StaticObstacles { get; }

    /// <summary>
    /// Starting positions of creatures.
    /// </summary>
    public List<Point> ObstaclePositions { get; }

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
    public IMappable CurrentCreature 
    {
        get /* implement getter only */
        {
            return Creatures[_currentCreatureIndex % Creatures.Count];
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
    public Simulation(Map map, List<IMappable> mappables, List<Point> positions, List<StaticObstacle> staticObstacles, List<Point> obstaclePositions, string moves)
    { 
        Map = map;
        if (mappables.Count == 0)
        {
            throw new ArgumentException("List of Creatures cannot be empty", nameof(mappables));
        }
        else if (mappables.Count != positions.Count)
        {
            throw new ArgumentException("Number of creatures and their starting positions must match", nameof(positions));
        }
        else
        {
            Creatures = mappables;
            Positions = positions;
            StaticObstacles = staticObstacles;
            ObstaclePositions = obstaclePositions;  
            Moves = moves;
            _currentTurnIndex = 0;
            _currentCreatureIndex = 0;
            _numberOfTurns = _directionListForSimulation.Count;
            // Inicjowanie stworów na mapie
            int i = 0; // Positions iterator
            foreach (IMappable mappable in Creatures)
            {
                mappable.InitMapAndPosition(Map, Positions[i]);
                i++;
            }
            int j = 0;
            foreach (IMappable mappable in StaticObstacles)
            {
                mappable.InitMapAndPosition(Map, ObstaclePositions[j]);
                j++;
            }
        }
    }

    /// <summary>
    /// Makes one move of current creature in current direction.
    /// Throw error if simulation is finished.
    /// </summary>
    public void Turn() 
    {
        simulationMessage = false;
        if (CurrentCreature.IsLost)
        {
            CurrentCreature.RandomMove();
        }
        else
        {
            CurrentCreature.Go(_directionListForSimulation[_currentTurnIndex]);
        }
        


        if (ObstaclePositions.Contains(CurrentCreature.Position)) 
        {
            int obstacleIndex = ObstaclePositions.IndexOf(CurrentCreature.Position);
            StaticObstacle currentObstacle = StaticObstacles[obstacleIndex];

            switch (currentObstacle.NaturalElement)
            {
                case NaturalElement.Water:
                    endingMessage = $"{CurrentCreature} has drown...";
                    Drown();
                    break;
                case NaturalElement.Earth:
                    endingMessage = $"{CurrentCreature} cannot go this direction...";
                    RevertMove();
                    break;
                case NaturalElement.Air:
                    endingMessage = $"{CurrentCreature} got lost in fog...";
                    CurrentCreature.IsLost = true;
                    break;
            }
            simulationMessage = true;

        }
        if (Creatures.Count == 0)
        {
            this.Finished = true;
            endingMessage = "All creatures and animals are dead....";
            simulationMessage = true;
        }



        _currentTurnIndex++; 
        _currentCreatureIndex++;
        if (_creatureKilled)
        {
            _currentCreatureIndex--;
            _creatureKilled = false;
        }

        if (_currentTurnIndex == _numberOfTurns)
        {
            this.Finished = true;
            endingMessage = "At the end of simulation you still have some living creatures or animals! WELL DONE!";
            simulationMessage = true;
        }
    }

    public void Drown()
    {
       Map.Remove(CurrentCreature, CurrentCreature.Position);
       Creatures.Remove(CurrentCreature);
        _creatureKilled = true;
    }

    public void RevertMove()
    {
        switch (CurrentMoveName)
        {
            case "up":
                CurrentCreature.Go(Direction.Down);
                break;
            case "right":
                CurrentCreature.Go(Direction.Left);
                break;
            case "down":
                CurrentCreature.Go(Direction.Up);
                break;
            case "left":
                CurrentCreature.Go(Direction.Right);
                break;
        }
    }
}
