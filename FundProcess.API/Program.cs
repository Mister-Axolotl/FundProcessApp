using FundProcessLibrary;

namespace FundProcess.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IPerformanceProcessor, PerformanceProcessor>();

            var app = builder.Build();

            app.MapPost("/api/performance/calculate", (PerformanceRequest request, IPerformanceProcessor performanceProcessor) =>
            {
                if (request.Dataset == null || request.Dataset.Count < 2) return Results.BadRequest("Dataset must contain at least two data points.");

                var result = performanceProcessor.GetPerformance(request.Dataset.ToArray(), request.FromDate, request.ToDate);
                return Results.Ok(result);
            });

            app.Run();
        }
    }
    public class PerformanceRequest
    {
        public List<Tuple<DateTime, double>> Dataset { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}