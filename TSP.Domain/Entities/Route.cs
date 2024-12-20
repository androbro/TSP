using TSP.Domain.Enums;

namespace TSP.Domain.Entities;

public class Route
{
    public int Id { get; set; }
    public List<Point> Points { get; set; }
    public List<Connection> Connections { get; set; } = [];
    public double TotalDistance { get; set; }
    public OptimizationAlgorithm  Algorithm { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CalculationTime { get; set; }

    public Route( List<Point> points, OptimizationAlgorithm algorithm)
    {
        Points = points;
        Algorithm = algorithm;
        CreatedAt = DateTime.Now;
    }
    
    public bool IsValidAlgorithm()
    {
        return Enum.IsDefined(typeof(OptimizationAlgorithm), Algorithm);
    }
}