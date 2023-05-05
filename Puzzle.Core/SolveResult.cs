namespace Puzzle.Core;

public class SolveResult
{
    public string Solution { get; set; } = null!;
    public int AmountOfVisitedStates { get; set; }
    public int AmountOfProcessedStates { get; set; }
    public int Depth { get; set; }
}