using RobotSimulator.Application.Commands;
using RobotSimulator.Domain.Enums;
using RobotSimulator.Domain.Interfaces;
using RobotSimulator.Domain.Models;

namespace RobotSimulator.Application.Parsing;

public class CommandParser(IReportOutputHandler outputHandler) : ICommandParser
{
    public ICommand? Parse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return null;

        var trimmed = input.Trim();

        if (trimmed.StartsWith("//"))
            return null;

        if (trimmed.StartsWith("PLACE "))
            return ParsePlace(trimmed[6..]);

        return trimmed switch
        {
            "MOVE"   => new MoveCommand(),
            "LEFT"   => new LeftCommand(),
            "RIGHT"  => new RightCommand(),
            "REPORT" => new ReportCommand(outputHandler),
            _        => null
        };
    }

    private static PlaceCommand? ParsePlace(string args)
    {
        var parts = args.Split(',');
        if (parts.Length != 3) return null;

        if (!int.TryParse(parts[0], out var x)) return null;
        if (!int.TryParse(parts[1], out var y)) return null;
        if (!Enum.TryParse<Direction>(parts[2], ignoreCase: true, out var direction)) return null;

        return new PlaceCommand(new Position(x, y), direction);
    }
}