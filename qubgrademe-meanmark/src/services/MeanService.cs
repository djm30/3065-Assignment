using System.Collections.Generic;
using System.Linq;
namespace Assignment2.MeanMark.Services;

public class MeanService : IMeanService
{
    public double CalculateMean(List<string> marks)
    {
        // int count = 0;
        // int sum = marks.Aggregate(0, (sum, next) =>
        // {
        //     int mark;
        //     if (int.TryParse(next, out mark))
        //     {
        //         count++;
        //         return sum + mark;  
        //     }
        //     return sum;
        // });
        // return (double)sum / count;
        
        var count = 0;
        var sum = marks.Where(x => x.Trim() != "").Aggregate(0, (total, next) =>
        {
            count++;
            return total + int.Parse(next);
        });
        return count == 0 ? 0 : (double)sum / count;
    }
    
}