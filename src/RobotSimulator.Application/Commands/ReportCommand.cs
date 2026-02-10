using RobotSimulator.Domain.Interfaces;

namespace RobotSimulator.Application.Commands;

public class ReportCommand(IReportOutputHandler outputHandler) : ICommand
{
    public void Execute(IRobot robot)
    {
        var result = robot.Report();
        if (result is not null)
            outputHandler.Handle(result);
    }
}