using RobotSimulator.Domain.Enums;
using RobotSimulator.Domain.Interfaces;
using RobotSimulator.Domain.Models;

namespace RobotSimulator.Application;

public class Robot : IRobot
{
    public bool IsPlaced => CurrentPosition is not null;
    public Position? CurrentPosition { get; private set; }
    public Direction? CurrentDirection { get; private set; }

    public void Place(Position position, Direction direction)
    {
    }

    public void Move()
    {
    }

    public void TurnLeft()
    {
    }

    public void TurnRight()
    {
    }

    public string? Report()
    {
        return null;
    }
}