using System.Collections.Generic;
using Assignment2.MeanMark.Types;

namespace Assignment2.MeanMark.Services;

public interface IValidator
{
    public ValidationResponse Validate(List<string> modules, List<string> marks);
}