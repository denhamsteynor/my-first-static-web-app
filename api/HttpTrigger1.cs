using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class HttpTrigger1
    {
        private readonly ILogger<HttpTrigger1> _logger;
        private readonly ApplicationDbContext _context;
        public HttpTrigger1(ILogger<HttpTrigger1> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Function("GetSensorReadings")]
        public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
                        try
            {
                var queryParams = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
                string sensorType = queryParams["sensorType"];
                string countParam = queryParams["count"];
                int count = int.TryParse(countParam, out int parsedCount) ? parsedCount : 10; // Default to 10 records
;
                IQueryable<SensorData> query = _context.SensorReadings;

                    var readings = await _context.SensorData
                        .Where(r => r.SensorType == sensorType)
                        .OrderByDescending(r => r.LogDate)
                        .Take(count)
                        .ToListAsync();
     

 ;

                HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);
                response.Headers.Add("Content-Type", "application/json");
                await response.WriteStringAsync(JsonSerializer.Serialize(readings));
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching sensor readings: {ex.Message}");

                var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
                await errorResponse.WriteStringAsync("An error occurred while fetching data.");
                return errorResponse;
            }
        }
    }
}
