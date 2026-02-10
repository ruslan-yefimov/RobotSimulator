using RobotSimulator.Domain.Enums;
using RobotSimulator.Domain.Models;

namespace RobotSimulator.Domain.Interfaces;

public interface IRobot
{
    bool IsPlaced { get; }
    Position? CurrentPosition { get; }
    Direction? CurrentDirection { get; }

    void Place(Position position, Direction direction);
    void Move();
    void TurnLeft();
    void TurnRight();
    string? Report();
}