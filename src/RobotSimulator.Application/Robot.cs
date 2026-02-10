using RobotSimulator.Domain.Enums;
using RobotSimulator.Domain.Interfaces;
using RobotSimulator.Domain.Models;

namespace RobotSimulator.Application;

public class Robot : IRobot
{
    private const int TableSize = 5;

    public bool IsPlaced => CurrentPosition is not null;
    public Position? CurrentPosition { get; private set; }
    public Direction? CurrentDirection { get; private set; }

    public void Place(Position position, Direction direction)
    {
        if (!IsValidPosition(position)) return;
        CurrentPosition = position;
        CurrentDirection = direction;
    }

    public void Move()
    {
        if (!IsPlaced) return;

        var (dx, dy) = CurrentDirection!.Value switch
        {
            Direction.North => (0, 1),
            Direction.East  => (1, 0),
            Direction.South => (0, -1),
            Direction.West  => (-1, 0),
            _ => (0, 0)
        };

        var next = new Position(CurrentPosition!.Value.X + dx, CurrentPosition.Value.Y + dy);

        if (IsValidPosition(next))
            CurrentPosition = next;
    }

    public void TurnLeft()
    {
        if (!IsPlaced) return;

        CurrentDirection = CurrentDirection!.Value switch
        {
            Direction.North => Direction.West,
            Direction.West  => Direction.South,
            Direction.South => Direction.East,
            Direction.East  => Direction.North,
            _ => CurrentDirection
        };
    }

    public void TurnRight()
    {
        if (!IsPlaced) return;

        CurrentDirection = CurrentDirection!.Value switch
        {
            Direction.North => Direction.East,
            Direction.East  => Direction.South,
            Direction.South => Direction.West,
            Direction.West  => Direction.North,
            _ => CurrentDirection
        };
    }

    public string? Report()
    {
        if (!IsPlaced) return null;
        return $"{CurrentPosition!.Value.X},{CurrentPosition.Value.Y},{CurrentDirection!.Value.ToString().ToUpper()}";
    }

    private static bool IsValidPosition(Position p) =>
        p.X >= 0 && p.X < TableSize && p.Y >= 0 && p.Y < TableSize;
}