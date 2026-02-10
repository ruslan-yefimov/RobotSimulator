using RobotSimulator.Application;
using RobotSimulator.Application.Execution;
using RobotSimulator.Application.Parsing;
using RobotSimulator.Domain.Enums;
using RobotSimulator.Domain.Interfaces;
using RobotSimulator.Domain.Models;
using NSubstitute;
using Shouldly;

namespace RobotSimulator.UnitTests;

public class RobotTests
{
    private readonly Robot _robot = new();

    [Fact]
    public void Place_SetsPositionAndDirection()
    {
        _robot.Place(new Position(1, 2), Direction.North);

        _robot.IsPlaced.ShouldBeTrue();
        _robot.CurrentPosition.ShouldBe(new Position(1, 2));
        _robot.CurrentDirection.ShouldBe(Direction.North);
    }

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(0, -1)]
    [InlineData(5, 0)]
    [InlineData(0, 5)]
    [InlineData(5, 5)]
    public void Place_OutOfBounds_IsIgnored(int x, int y)
    {
        _robot.Place(new Position(x, y), Direction.North);

        _robot.IsPlaced.ShouldBeFalse();
    }

    [Theory]
    [InlineData(Direction.North, 0, 1)]
    [InlineData(Direction.East, 1, 0)]
    [InlineData(Direction.South, 0, -1)]
    [InlineData(Direction.West, -1, 0)]
    public void Move_AdvancesOneUnitInFacingDirection(Direction facing, int dx, int dy)
    {
        _robot.Place(new Position(2, 2), facing);

        _robot.Move();

        _robot.CurrentPosition.ShouldBe(new Position(2 + dx, 2 + dy));
    }

    [Theory]
    [InlineData(0, 0, Direction.South)]
    [InlineData(0, 0, Direction.West)]
    [InlineData(4, 4, Direction.North)]
    [InlineData(4, 4, Direction.East)]
    public void Move_WouldFallOff_IsIgnored(int x, int y, Direction facing)
    {
        _robot.Place(new Position(x, y), facing);

        _robot.Move();

        _robot.CurrentPosition.ShouldBe(new Position(x, y));
    }

    [Theory]
    [InlineData(Direction.North, Direction.West)]
    [InlineData(Direction.West, Direction.South)]
    [InlineData(Direction.South, Direction.East)]
    [InlineData(Direction.East, Direction.North)]
    public void TurnLeft_RotatesCounterClockwise(Direction from, Direction expected)
    {
        _robot.Place(new Position(0, 0), from);

        _robot.TurnLeft();

        _robot.CurrentDirection.ShouldBe(expected);
    }

    [Theory]
    [InlineData(Direction.North, Direction.East)]
    [InlineData(Direction.East, Direction.South)]
    [InlineData(Direction.South, Direction.West)]
    [InlineData(Direction.West, Direction.North)]
    public void TurnRight_RotatesClockwise(Direction from, Direction expected)
    {
        _robot.Place(new Position(0, 0), from);

        _robot.TurnRight();

        _robot.CurrentDirection.ShouldBe(expected);
    }

    [Fact]
    public void CommandsBeforePlacement_AreIgnored()
    {
        _robot.Move();
        _robot.TurnLeft();
        _robot.TurnRight();
        _robot.Report().ShouldBeNull();

        _robot.IsPlaced.ShouldBeFalse();
    }

    // --- Integration tests: full command sequences through real parser + executor ---

    [Fact]
    public void Integration_SpecExample1_PlaceMoveReport()
    {
        // PLACE 0,0,NORTH → MOVE → REPORT → "0,1,NORTH"
        var output = RunCommands("PLACE 0,0,NORTH", "MOVE", "REPORT");

        output.ShouldBe(["0,1,NORTH"]);
    }

    [Fact]
    public void Integration_SpecExample2_PlaceLeftReport()
    {
        // PLACE 0,0,NORTH → LEFT → REPORT → "0,0,WEST"
        var output = RunCommands("PLACE 0,0,NORTH", "LEFT", "REPORT");

        output.ShouldBe(["0,0,WEST"]);
    }

    [Fact]
    public void Integration_CommandsBeforePlace_AreDiscarded()
    {
        var output = RunCommands("MOVE", "LEFT", "REPORT", "PLACE 3,3,SOUTH", "MOVE", "REPORT");

        output.ShouldBe(["3,2,SOUTH"]);
    }

    [Fact]
    public void Integration_MultiplePlacements_LastWins()
    {
        var output = RunCommands("PLACE 0,0,NORTH", "PLACE 2,3,EAST", "REPORT");

        output.ShouldBe(["2,3,EAST"]);
    }

    [Fact]
    public void Integration_BoundaryProtection_RobotSurvives()
    {
        // Place at edge, try to walk off, then continue
        var output = RunCommands(
            "PLACE 4,4,NORTH",
            "MOVE",       // would fall — ignored
            "RIGHT",
            "MOVE",       // would fall — ignored
            "REPORT"
        );

        output.ShouldBe(["4,4,EAST"]);
    }

    private static List<string> RunCommands(params string[] commands)
    {
        var captured = new List<string>();
        var outputHandler = Substitute.For<IReportOutputHandler>();
        outputHandler
            .When(x => x.Handle(Arg.Any<string>()))
            .Do(ci => captured.Add(ci.Arg<string>()));

        var parser = new CommandParser(outputHandler);
        var executor = new CommandExecutor(parser);
        var robot = new Robot();

        executor.Execute(commands, robot);

        return captured;
    }
}