using TSP.Domain.Entities;

namespace TSP.Application.DTOs;

public class ConnectionDto
{
    public Point FromPoint { get; set; }  // Index of starting point
    public Point ToPoint { get; set; }    // Index of ending point
        
    // Optional: Added properties for visualization
    public double Distance { get; set; }    // Distance between points
    public bool IsOptimal { get; set; }     // Flag for highlighting optimized segments
}