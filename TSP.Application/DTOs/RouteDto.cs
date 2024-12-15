namespace TSP.Application.DTOs;

public class RouteDto
{
    public int Id { get; set; }
    public ICollection<PointDto> Points { get; set; }
    public ICollection<ConnectionDto> Connections { get; set; }
    public double TotalDistance { get; set; }
    public OptimizationAlgorithmDto AlgorithmDto { get; set; } 
    public string CalculationTime { get; set; } 
    
}