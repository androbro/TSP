// using TSP.Application.Services.RouteOptimization.Interfaces;
// using TSP.Domain.Entities;
//
// namespace TSP.Application.Services.RouteOptimization.Strategies;
//
// public class BruteForceStrategy : IRouteOptimizationStrategy
// {
//     public string algorithmName { get; }
//     public Route? route { get; set; }
//     
//     public Route OptimizeRoute(List<Point> points)
//     {
//         //example: [a,b,c]
//         //route: [a-b-c], [a-c-b], [b-a-c]
//         
//         //lus: elk punt moet 2 connecties hebben 
//         //lus: elk punt mag maar 1 keer voorkomen in de lus
//         //lus: lus moet uniek zijn in connecties
//
//         Dictionary<List<Point>, double> routeDistances = new();
//
//         foreach (var route in GeneratePermutations(points))
//         {
//             List<Connection> connections = [];
//             for (int i = 0; i < route.Count; i++)
//             {
//                 if (i == route.Count - 1)
//                 {
//                     var lastConnection = new Connection(route[i], route[0]);
//                     connections.Add(lastConnection);
//                     break;
//                 }
// //wayfunction collapse
//                 var start = route[i];
//                 var end = route[i + 1];
//                 var connection = new Connection(start, end);
//
//                 connections.Add(connection);
//             }
//             routeDistances.Add(route, connections.Sum(x => x.Distance));
//         }
//
//         var bestRoute = routeDistances.OrderBy(x => x.Value).First().Key;
//
//         return new Route(bestRoute);
//     }
//     
//
//     private IEnumerable<List<Point>> GeneratePermutations(List<Point> points)
//     {
//         // Base case: if there is only one point or no points, return the list as is
//         if (points.Count <= 1)
//         {
//             yield return points;
//             yield break;
//         }
//
//         // Take the first point
//         var firstPoint = points[0];
//         // Get the remaining points
//         var remainingPoints = points.Skip(1).ToList();
//
//         // Recursively generate all permutations of the remaining points
//         foreach (var permutation in GeneratePermutations(remainingPoints))
//         {
//             // Insert the first point into every possible position of each permutation
//             for (var i = 0; i <= permutation.Count; i++)
//             {
//                 var newRoute = new List<Point>(permutation);
//                 newRoute.Insert(i, firstPoint);
//                 yield return newRoute;
//             }
//         }
//     }
// }
//
//
// record Connection(Point From, Point To)
// {
//     private double? _distance;
//     public double Distance => _distance ??= CalculateDistance();
//     
//     private double CalculateDistance()
//     {
//         return Math.Sqrt(Math.Pow(From.X - To.X, 2) + Math.Pow(From.Y - To.Y, 2));
//     }
// }