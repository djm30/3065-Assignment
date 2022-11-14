using src.Data;

namespace src.Services;

public class LogResults
{
    
    public LogResults(MonitoringService monitoringService)
    {
        monitoringService.ServiceStatusChanged += OnServiceStatusChanged;
    }

    private void OnServiceStatusChanged(object? sender, List<ServiceMonitorSchema> e)
    {
        LogData(e);
    }
    
    private void LogData(List<ServiceMonitorSchema> serviceMonitorSchemata)
    {
        // Create a new instance of the StreamWriter class
        // to write data to the file.
        using var sw = File.AppendText("./logs/monitor_results.txt");
        // Write a line of text to the file.
        // Write the datetime in square brackets
        sw.Write("[  START  ]");
        sw.Write("[");
        sw.Write(DateTime.Now);
        sw.Write("] ");
        foreach (var serviceMonitorSchema in serviceMonitorSchemata)
        {
            sw.WriteLine(serviceMonitorSchema.name);
            foreach (var service in serviceMonitorSchema.services)
            {
                // write service url, response, status and is expected_value
                sw.Write(service.url);
                sw.Write(" ");
                sw.Write(service.responseTime);
                sw.Write(" ");
                sw.Write(service.statusCode);
                sw.Write(" ");
                sw.Write(service.isExpected);
                sw.WriteLine();
            }
            sw.WriteLine();
        }
        // Write end in square brackets
        sw.Write("[   END   ]");
        // Close the file.
        sw.Close();
    }
}