using Moonship_Orlov_M3216;

namespace Moonship_Orlov_M3216;

public class Calculations
{
    public Parameters Num = new Parameters();

    public double CountFallSpeed(double t)
    {
        return Num.StartSpeed + Num.Gravity * t;
    }

    public double CountSpeedWithWorkingEngine(double ft, double t)
    {
        var fallSpeed = CountFallSpeed(ft);
        var fuelMassRemaining = Num.TotalMass - Num.BurnSpeed * t;
        return fallSpeed + Num.Gravity * t - Num.JetSpeed * Math.Log(Num.TotalMass / fuelMassRemaining);
    }

    public double CountFallHeight(double t)
    {
        return Num.StartSpeed * t + Num.Gravity * Math.Pow(t, 2) / 2.0;
    }

    public double CountHeightWithWorkingEngine(double ft, double t)
    {
        var t1 = CountFallSpeed(ft) * t;
        var t2 = Num.Gravity * Math.Pow(t, 2) / 2.0;
        var fuelMassRemaining = Num.TotalMass - Num.BurnSpeed * t;
        var t3 = Math.Log(Num.TotalMass) * t;
        var t4 = fuelMassRemaining * Math.Log(fuelMassRemaining) -
                 Num.TotalMass * Math.Log(Num.TotalMass) +
                 Num.BurnSpeed * t;

        return t1 + t2 - Num.JetSpeed * (t3 + (1 / Num.BurnSpeed) * t4);
    }

    public double CountAccelerationWithWorkingEngine(double t)
    {
        var fuelMassRemaining = Num.TotalMass - Num.BurnSpeed * t;
        return Num.Gravity - Num.JetSpeed * (Num.BurnSpeed / fuelMassRemaining);
    }

    public void FindVariableTime()
    {
        for (var i = 1.0; CountFallHeight(i) <= Num.StartHeight; i += 0.001)
        {
            for (var j = 0.5; j <= 10; j += 0.05)
            {
                var heightDifference = CountHeightWithWorkingEngine(i, j) + CountFallHeight(i) - Num.StartHeight;
                var finalSpeed = CountSpeedWithWorkingEngine(i, j);

                if (!(Math.Abs(heightDifference) <= 0.5) || !(finalSpeed >= 0.0) || !(finalSpeed <= 3.0)) continue;
                Num.FallTime = i;
                Num.EngineTime = j;
                return;
            }
        }
    }
}