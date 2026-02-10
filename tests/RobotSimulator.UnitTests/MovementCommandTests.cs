using RobotSimulator.Application.Commands;
using RobotSimulator.Domain.Enums;
using RobotSimulator.Domain.Interfaces;
using RobotSimulator.Domain.Models;
using NSubstitute;
using Shouldly;

namespace RobotSimulator.UnitTests;

public class MovementCommandTests
{
    private readonly IRobot _robot = Substitute.For<IRobot>();

    [Fact]
    public void PlaceCommand_CallsPlaceWithCorrectArgs()
    {
        var command = new PlaceCommand(new Position(3, 4), Direction.West);

        command.Execute(_robot);

        _robot.Received(1).Place(new Position(3, 4), Direction.West);
    }

    [Fact]
    public void MoveCommand_CallsMove()
    {
        new MoveCommand().Execute(_robot);

        _robot.Received(1).Move();
    }

    [Fact]
    public void LeftCommand_CallsTurnLeft()
    {
        new LeftCommand().Execute(_robot);

        _robot.Received(1).TurnLeft();
    }

    [Fact]
    public void RightCommand_CallsTurnRight()
    {
        new RightCommand().Execute(_robot);

        _robot.Received(1).TurnRight();
    }
}