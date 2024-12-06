namespace Moonship_Orlov_M3216;

public class Parameters
{
    public double ObjectMass { get; set; } = 2150.0;
    
    public double FuelMass { get; set; } = 150.0;
    
    public double JetSpeed { get; set; } = 3660.0;
    
    public double BurnSpeed { get; set; } = 15.0;
    
    public double Gravity { get; set; } = 1.62;
    
    public double StartSpeed { get; set; } = 0;
    
    public double StartHeight { get; set; } = 0;
    
    public double FallTime { get; set; } = 0;
    
    public double EngineTime { get; set; } = 0;
    
    public double TotalMass => ObjectMass + FuelMass;
}