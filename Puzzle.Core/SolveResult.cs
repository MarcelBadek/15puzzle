namespace Puzzle.Core;

public class SolveResult
{
    public string Solution { get; set; } = null!;
    public int AmountOfVisitedStates { get; set; }
    public int AmountOfProcessedStates { get; set; }
    public int MaxDepth { get; set; }

    public void CreateSolutionFile(string fileName)
    {
        var dict = Directory.GetCurrentDirectory();
        var fullPath = Path.Combine(dict, fileName);
        using var file = new StreamWriter(fullPath);
        file.WriteLine(Solution.Length);
        file.WriteLine(Solution);
    }

    public void CreateStatisticsFile(string fileName, double time)
    {
        var dict = Directory.GetCurrentDirectory();
        var fullPath = Path.Combine(dict, fileName);
        using var file = new StreamWriter(fullPath);
        file.WriteLine(Solution.Length);
        file.WriteLine(AmountOfVisitedStates);
        file.WriteLine(AmountOfProcessedStates);
        file.WriteLine(MaxDepth);
        file.WriteLine(time.ToString("F3"));
    }
}