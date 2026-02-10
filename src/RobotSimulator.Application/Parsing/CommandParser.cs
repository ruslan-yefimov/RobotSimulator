using RobotSimulator.Domain.Interfaces;

namespace RobotSimulator.Application.Parsing;

public class CommandParser(IReportOutputHandler reportOutputHandler) : ICommandParser
{
    public ICommand? Parse(string input)
    {
        return null;
    }
}