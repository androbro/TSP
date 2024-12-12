using MediatR;
using TSP.Application.DTOs;

namespace TSP.Application.UseCases.Map.Commands.CreateMap;

public class CreateMapCommandHandler : IRequestHandler<CreateMapCommand, List<PointDto>>
{
    private readonly Random _random;

    public CreateMapCommandHandler()
    {
        _random = new Random();
    }

    public Task<List<PointDto>> Handle(CreateMapCommand request, CancellationToken cancellationToken)
    {
        var points = new List<PointDto>();
        var usedCoordinates = new HashSet<(int x, int y)>(); // To ensure unique points

        while (points.Count < request.NumberOfPoints)
        {
            var x = _random.Next(0, request.GridSize);
            var y = _random.Next(0, request.GridSize);

            if (usedCoordinates.Add((x, y))) // Returns true if the coordinate was added (i.e., was unique)
            {
                points.Add(new PointDto 
                { 
                    Id = Guid.NewGuid(),
                    X = x, 
                    Y = y 
                });
            }
        }

        return Task.FromResult(points);
    }
}