namespace TSP.Domain.Entities;

public class Point(double x, double y)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public double X { get; set; } = x;
    public double Y { get; set; } = y;
}