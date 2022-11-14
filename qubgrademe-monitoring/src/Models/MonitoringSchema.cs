using System.Net;

namespace src.Data;
using System.Linq;

public class MonitoringSchema
{
    public string name { get; set; }
    public string url { get; set; }
    public HttpStatusCode statusCode { get; set; }
    public bool isExpected { get; set; }
    public int responseTime { get; set; }
    public string actual_result { get; set; }
}

public class ServiceMonitorSchema
{
    public string name { get; set; }
    public List<MonitoringSchema> services { get; set; }
    
    public string expected_value { get; set; }

    public AggregateSchema AggregateData()
    {
        var responseTime = services.Count() != 0 ? services.Sum(x => x.responseTime) / services.Count() : 0;
        var servicesUp = services.Count(x => (int)x.statusCode >= 200 && (int)x.statusCode <= 300);
        var servicesCorrect = services.Count(x => x.isExpected);

        return new AggregateSchema()
        {
            averageResponseTime = responseTime,
            servicesUp = $"{servicesUp}/{services.Count}",
            servicesCorrect = $"{servicesCorrect}/{services.Count}"
        };
    }
}