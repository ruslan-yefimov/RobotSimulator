// Composition root

using RobotSimulator.Application;
using RobotSimulator.Application.Execution;
using RobotSimulator.Application.Output;
using RobotSimulator.Application.Parsing;

var outputHandler = new ConsoleReportOutputHandler();
var parser = new CommandParser(outputHandler);
var executor = new CommandExecutor(parser);
var robot = new Robot();

IEnumerable<string> lines = args.Length > 0
    ? File.ReadLines(args[0])
    : ReadStdin();

executor.Execute(lines, robot);

static IEnumerable<string> ReadStdin()
{
    string? line;
    while ((line = Console.ReadLine()) is not null)
        yield return line;
}
