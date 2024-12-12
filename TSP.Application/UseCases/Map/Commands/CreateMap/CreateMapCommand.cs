using MediatR;
using TSP.Application.DTOs;

namespace TSP.Application.UseCases.Map.Commands.CreateMap;

public class CreateMapCommand: IRequest<List<PointDto>>
{
    public int NumberOfPoints { get; init; }
    public int GridSize { get; init; } = 100; // Default grid size
}