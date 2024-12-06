using OxyPlot;
using OxyPlot.Series;

namespace Moonship_Orlov_M3216;

public class GraphGenerator(Calculations calculations, Parameters num)
{
    public void GenerateGraphs()
    {
        var speedSeries = new LineSeries { Title = "Speed (Vy)" };
        var heightSeries = new LineSeries { Title = "Height (H)" };
        var accelerationSeries = new LineSeries { Title = "Acceleration (Ay)" };

        var totalTime = num.FallTime + num.EngineTime;
        for (double t = 0; t <= totalTime; t += 0.1)
        {
            var speed = t <= num.FallTime
                ? calculations.CountFallSpeed(t)
                : calculations.CountSpeedWithWorkingEngine(num.FallTime, t - num.FallTime);
            var height = t <= num.FallTime
                ? calculations.CountFallHeight(t)
                : calculations.CountFallHeight(num.FallTime) +
                  calculations.CountHeightWithWorkingEngine(num.FallTime, t - num.FallTime);
            var acceleration = t <= num.FallTime
                ? num.Gravity
                : calculations.CountAccelerationWithWorkingEngine(t - num.FallTime);

            speedSeries.Points.Add(new OxyPlot.DataPoint(t, speed));
            heightSeries.Points.Add(new OxyPlot.DataPoint(t, height));
            accelerationSeries.Points.Add(new OxyPlot.DataPoint(t, acceleration));
        }

        SavePlot(speedSeries, "SpeedGraph.png");
        SavePlot(heightSeries, "HeightGraph.png");
        SavePlot(accelerationSeries, "AccelerationGraph.png");
    }

    private void SavePlot(LineSeries series, string fileName)
    {
        var plotModel = new PlotModel { Title = series.Title };
        plotModel.Series.Add(series);

        var parentDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName;
        var path = Path.Combine(parentDirectory, fileName);
        
        using (var stream = File.Create(path))
        {
            var exporter = new OxyPlot.SkiaSharp.PngExporter { Width = 600, Height = 400 };
            exporter.Export(plotModel, stream);
        }

        Console.WriteLine($"График {series.Title} сохранен как {fileName}");
    }
}