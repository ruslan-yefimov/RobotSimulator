using RobotSimulator.Domain.Interfaces;

namespace RobotSimulator.Application.Commands;

public class ReportCommand(IReportOutputHandler outputHandler) : ICommand
{
    public void Execute(IRobot robot)
    {
        outputHandler.Handle(robot.Report() ?? "");
    }
}