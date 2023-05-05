namespace Puzzle.Core;

public class SolveResult
{
    public string Solution { get; set; } = null!;
    public int AmountOfVisitedStates { get; set; }
    public int AmountOfProcessedStates { get; set; }
    public int MaxDepth { get; set; }

    public void CreateSolutionFile(string fileName)
    {
        using var file = new StreamWriter(fileName);
        file.WriteLine(Solution.Length);
        file.WriteLine(Solution);
    }

    public void CreateStatisticsFile(string fileName, double time)
    {
        using var file = new StreamWriter(fileName);
        file.WriteLine(Solution.Length);
        file.WriteLine(AmountOfVisitedStates);
        file.WriteLine(AmountOfProcessedStates);
        file.WriteLine(MaxDepth);
        file.WriteLine(time.ToString("F3"));
    }
}