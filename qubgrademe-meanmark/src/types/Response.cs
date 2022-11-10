using System.Collections.Generic;

namespace Assignment2.MeanMark.Types;

public class Response
{
    public bool error { get; set; }
    public string errorMessage { get; set; }
    public List<string> modules { get; set; }
    public List<int> marks { get; set; }
    public double mean { get; set; }
}