using System.Collections.Generic;

namespace Assignment2.MeanMark.Services;

public interface IMeanService
{
    public double CalculateMean(List<string> marks);
}