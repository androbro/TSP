using TSP.Domain.Enums;

namespace TSP.Application.DTOs;

public class RouteDto
{
    public int Id { get; set; }
    public ICollection<PointDto> Points { get; set; }
    public ICollection<ConnectionDto> Connections { get; set; }
    public double TotalDistance { get; set; }
    public string Algorithm { get; set; } 
    public string CalculationTime { get; set; } 

    // Helper method to convert from domain enum
    public static string GetAlgorithmName(AlgorithmType type) => type.ToString();

    // Helper method to parse string back to enum
    public static AlgorithmType ParseAlgorithm(string algorithm)
    {
        return Enum.TryParse<AlgorithmType>(algorithm, out var result)
            ? result
            : throw new ArgumentException($"Invalid algorithm type: {algorithm}");
    }
}