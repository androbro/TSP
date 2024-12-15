using TSP.Application.Interfaces;
using TSP.Application.Services;
using TSP.Application.Services.RouteOptimization.Common;
using TSP.Application.Services.RouteOptimization.Interfaces;
using TSP.Application.Services.RouteOptimization.Strategies;
using TSP.Application.UseCases.Map.Commands.CreateMap;
using TSP.Application.UseCases.Route.Command.CreateRoute;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register MediatR handlers
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(CreateRouteCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(CreateRouteCommandHandler).Assembly);
});

// custom services
builder.Services.AddScoped<IRouteService, RouteService>();
builder.Services.AddScoped<IRouteStrategyFactory, RouteStrategyFactory>();

// Register optimization strategies
builder.Services.AddScoped<IRouteOptimizationStrategy, NearestNeighborStrategy>();
builder.Services.AddScoped<IRouteOptimizationStrategy, TwoOptStrategy>();
builder.Services.AddScoped<IRouteOptimizationStrategy, SimulatedAnnealingStrategy>();
builder.Services.AddScoped<IRouteOptimizationStrategy, GeneticAlgorithmStrategy>();
builder.Services.AddScoped<IRouteOptimizationStrategy, LinKernighanStrategy>();
builder.Services.AddScoped<IRouteOptimizationStrategy, BruteForceStrategy>();

//cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder =>
        {
            builder
                .WithOrigins(
                    "http://localhost:5173",     // Vite default
                    "https://localhost:5173",    // Vite HTTPS
                    "http://localhost:44374",    // backend port
                    "https://localhost:44374"    // backend HTTPS
                )
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});

var app = builder.Build();

app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.InjectStylesheet("/swagger-ui/custom.css");
    });
}

// Use CORS before other middleware
app.UseCors("AllowFrontend");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();