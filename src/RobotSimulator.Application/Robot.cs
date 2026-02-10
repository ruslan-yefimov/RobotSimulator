using RobotSimulator.Domain.Enums;
using RobotSimulator.Domain.Interfaces;
using RobotSimulator.Domain.Models;

namespace RobotSimulator.Application;

public class Robot : IRobot
{
    public bool IsPlaced { get; }
    public Position? CurrentPosition { get; }
    public Direction? CurrentDirection { get; }
    
    public void Place(Position position, Direction direction)
    {
        throw new NotImplementedException();
    }

    public void Move()
    {
        throw new NotImplementedException();
    }

    public void TurnLeft()
    {
        throw new NotImplementedException();
    }

    public void TurnRight()
    {
        throw new NotImplementedException();
    }

    public string? Report()
    {
        throw new NotImplementedException();
    }
}