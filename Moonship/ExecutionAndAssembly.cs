namespace Moonship_Orlov_M3216;

public class ExecutionAndAssembly
{
    private Calculations calculations = new Calculations();

    public void CalculateDataAndPlot(double startSpeed, double startHeight)
    {
        calculations.Num.StartSpeed = startSpeed;
        calculations.Num.StartHeight = startHeight;

        calculations.FindVariableTime();

        var num = calculations.Num;

        Console.WriteLine(
            $"Константы:\nМасса корабля и пилота: {num.ObjectMass} кг\nМасса топлива: {num.FuelMass} кг\nСкорость выброса частиц: {num.JetSpeed} м/с\nСкорость расхода топлива: {num.BurnSpeed} кг/с\nУскорение свободного падения: {num.Gravity} м/с^2\nНачальная скорость: {num.StartSpeed} м/с\nНачальная высота: {num.StartHeight} м\nВремя падения с выключенным двигателем: {num.FallTime} с\nВремя работы двигателя: {num.EngineTime} с\nОбщая масса: {num.TotalMass} кг");

        var finalSpeed =
            calculations.CountSpeedWithWorkingEngine(calculations.Num.FallTime, calculations.Num.EngineTime);
        var finalHeight = calculations.CountFallHeight(calculations.Num.FallTime) +
                          calculations.CountHeightWithWorkingEngine(calculations.Num.FallTime,
                              calculations.Num.EngineTime);

        Console.WriteLine(
            $"\nРезультаты:\nКонечная скорость: {finalSpeed:F15} м/с\nКонечная высота: {finalHeight:F15} м");

        var graphGenerator = new GraphGenerator(calculations, calculations.Num);

        graphGenerator.GenerateGraphs();
    }
    
    
}