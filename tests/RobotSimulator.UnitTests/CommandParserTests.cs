using RobotSimulator.Application.Commands;
using RobotSimulator.Application.Parsing;
using RobotSimulator.Domain.Enums;
using RobotSimulator.Domain.Interfaces;
using NSubstitute;
using Shouldly;

namespace RobotSimulator.UnitTests;

public class CommandParserTests
{
    private readonly CommandParser _parser = new(Substitute.For<IReportOutputHandler>());

    [Theory]
    [InlineData("MOVE", typeof(MoveCommand))]
    [InlineData("LEFT", typeof(LeftCommand))]
    [InlineData("RIGHT", typeof(RightCommand))]
    [InlineData("REPORT", typeof(ReportCommand))]
    public void Parse_SimpleCommands_ReturnsCorrectType(string input, Type expectedType)
    {
        var command = _parser.Parse(input);

        command.ShouldNotBeNull();
        command.ShouldBeOfType(expectedType);
    }

    [Theory]
    [InlineData("PLACE 0,0,NORTH", 0, 0, Direction.North)]
    [InlineData("PLACE 4,3,EAST", 4, 3, Direction.East)]
    [InlineData("PLACE 1,2,SOUTH", 1, 2, Direction.South)]
    public void Parse_PlaceCommand_ExtractsPositionAndDirection(
        string input, int expectedX, int expectedY, Direction expectedDir)
    {
        var command = _parser.Parse(input);

        var place = command.ShouldBeOfType<PlaceCommand>();
        place.Position.X.ShouldBe(expectedX);
        place.Position.Y.ShouldBe(expectedY);
        place.Direction.ShouldBe(expectedDir);
    }

    [Theory]
    [InlineData("")]
    [InlineData("JUMP")]
    [InlineData("PLACE")]
    [InlineData("PLACE 1,2")]
    [InlineData("PLACE a,b,NORTH")]
    [InlineData("PLACE 1,2,UP")]
    public void Parse_InvalidInput_ReturnsNull(string input)
    {
        _parser.Parse(input).ShouldBeNull();
    }
}