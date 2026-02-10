using RobotSimulator.Application.Execution;
using RobotSimulator.Domain.Interfaces;
using NSubstitute;
using Shouldly;

namespace RobotSimulator.UnitTests;

public class CommandExecutorTests
{
    private readonly ICommandParser _parser = Substitute.For<ICommandParser>();
    private readonly IRobot _robot = Substitute.For<IRobot>();
    private readonly CommandExecutor _executor;

    public CommandExecutorTests()
    {
        _executor = new CommandExecutor(_parser);
    }

    [Fact]
    public void Execute_ParsesAndExecutesEachLine()
    {
        var cmd1 = Substitute.For<ICommand>();
        var cmd2 = Substitute.For<ICommand>();
        _parser.Parse("A").Returns(cmd1);
        _parser.Parse("B").Returns(cmd2);

        _executor.Execute(["A", "B"], _robot);

        cmd1.Received(1).Execute(_robot);
        cmd2.Received(1).Execute(_robot);
    }

    [Fact]
    public void Execute_InvalidLine_SkippedGracefully()
    {
        var validCmd = Substitute.For<ICommand>();
        _parser.Parse("VALID").Returns(validCmd);
        _parser.Parse("GARBAGE").Returns((ICommand?)null);

        _executor.Execute(["GARBAGE", "VALID"], _robot);

        validCmd.Received(1).Execute(_robot);
    }

    [Fact]
    public void Execute_EmptyInput_DoesNothing()
    {
        _executor.Execute([], _robot);

        _parser.DidNotReceive().Parse(Arg.Any<string>());
    }
}