using RobotSimulator.Application.Commands;
using RobotSimulator.Domain.Interfaces;
using NSubstitute;
using Shouldly;

namespace RobotSimulator.UnitTests;

public class ReportCommandTests
{
    private readonly IRobot _robot = Substitute.For<IRobot>();
    private readonly IReportOutputHandler _outputHandler = Substitute.For<IReportOutputHandler>();

    [Fact]
    public void Execute_WhenPlaced_OutputsReport()
    {
        _robot.Report().Returns("1,2,NORTH");
        var command = new ReportCommand(_outputHandler);

        command.Execute(_robot);

        _outputHandler.Received(1).Handle("1,2,NORTH");
    }

    [Fact]
    public void Execute_WhenNotPlaced_DoesNotCallHandler()
    {
        _robot.Report().Returns((string?)null);
        var command = new ReportCommand(_outputHandler);

        command.Execute(_robot);

        _outputHandler.DidNotReceive().Handle(Arg.Any<string>());
    }
}