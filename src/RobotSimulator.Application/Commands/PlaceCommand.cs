using RobotSimulator.Domain.Enums;
using RobotSimulator.Domain.Interfaces;
using RobotSimulator.Domain.Models;

namespace RobotSimulator.Application.Commands;

public class PlaceCommand(Position position, Direction direction) : ICommand
{
    public Position Position => position;
    public Direction Direction => direction;

    public void Execute(IRobot robot)
    {
        robot.Place(Position, Direction);
    }
}