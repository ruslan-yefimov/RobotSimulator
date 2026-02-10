namespace RobotSimulator.Domain.Interfaces;

public interface ICommandParser
{
    ICommand? Parse(string input);
}