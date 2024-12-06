namespace Moonship_Orlov_M3216;

public class Program
{
    private static ExecutionAndAssembly _executionAndAssembly = new ExecutionAndAssembly();

    private static void Main(string[] args)
    {
        Console.WriteLine("Введите начальную скорость и высоту:");
        var input = Console.ReadLine()?.Split();
        if (input?.Length != 2 || !double.TryParse(input[0], out var startSpeed) ||
            !double.TryParse(input[1], out var startHeight))
        {
            Console.WriteLine("Некорректный ввод!");
            return;
        }
        
        _executionAndAssembly.CalculateDataAndPlot(startSpeed, startHeight);
    }
}