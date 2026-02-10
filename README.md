# Robot Simulator

A .NET 10 console application that simulates a toy robot moving on a 5×5 tabletop.

## Architecture

Multi-project solution using Command pattern, composition via interfaces, and dependency inversion.

```
src/
├── RobotSimulator.Domain         # Interfaces, enums, value objects — zero dependencies
├── RobotSimulator.Application    # Robot, commands, parser, executor implementations
└── RobotSimulator.Console        # Composition root and I/O
tests/
└── RobotSimulator.UnitTests      # xUnit + Shouldly + NSubstitute
```

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

## How to Run

> **Note:** These commands should be run from the solution root directory (`RobotSimulator/`).

**From a file (macOS/Linux):**

```bash
dotnet run --project src/RobotSimulator.Console -- commands.txt
```

**From a file (Windows):**

```powershell
dotnet run --project src\RobotSimulator.Console -- commands.txt
```

**From stdin (interactive):**

```bash
dotnet run --project src/RobotSimulator.Console
```

**From stdin (Windows):**

```powershell
dotnet run --project src\RobotSimulator.Console
```

Type commands one per line. Press `Ctrl+D` (macOS/Linux) or `Ctrl+Z` then Enter (Windows) to finish.

## Commands

| Command | Description |
|---|---|
| `PLACE X,Y,F` | Place robot at position (X,Y) facing NORTH, SOUTH, EAST, or WEST |
| `MOVE` | Move one unit forward in the current direction |
| `LEFT` | Rotate 90° counter-clockwise |
| `RIGHT` | Rotate 90° clockwise |
| `REPORT` | Output current position and direction |

Lines starting with `//` are treated as comments and ignored.

## Rules

- The table is 5×5 with origin (0,0) at the south-west corner.
- The first valid command must be `PLACE`. All commands before it are ignored.
- Moves that would cause the robot to fall off the table are silently ignored.
- `PLACE` can be issued multiple times to reposition the robot.

## Example

**commands.txt:**

```
PLACE 0,0,NORTH
MOVE
REPORT
```

**Output:**

```
0,1,NORTH
```

## Running Tests

```bash
dotnet test
```

On Windows:

```powershell
dotnet test
```