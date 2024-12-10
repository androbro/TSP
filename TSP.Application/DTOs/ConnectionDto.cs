namespace TSP.Application.DTOs;

public class ConnectionDto
{
    public int FromIndex { get; set; }  // Index of starting point
    public int ToIndex { get; set; }    // Index of ending point
        
    // Optional: Added properties for visualization
    public double Distance { get; set; }    // Distance between points
    public bool IsOptimal { get; set; }     // Flag for highlighting optimized segments
}