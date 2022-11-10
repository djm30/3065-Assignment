using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Assignment2.MeanMark.Types;

[assembly: InternalsVisibleTo("tests")]
namespace Assignment2.MeanMark.Services;

public  class Validator : IValidator
{
    public ValidationResponse Validate(List<string> modules, List<string> marks)
    {

        var response = new ValidationResponse() { error = false, errorMessage = "" };
        // Checking to see if all modules are empty
        if (modules.Count() == modules.Count(x => !IsStringValidModule(x)))
        {
            response.error = true;
            response.errorMessage = "Please provide some module codes and their respective marks";
        }

        if (!response.error)
        {
            for (int i = 0; i < modules.Count(); i++)
            {
                // Check if valid module at this index
                if (IsStringValidModule(modules[i]))
                {
                    // Check if mark is invalid
                    if (!IsMarkValidInteger(marks[i]))
                    {
                       response.error = true;
                       response.errorMessage = "Please provide a valid integer for every entered module";
                       break;
                    }
                }
                else
                {
                    if (IsMarkValidInteger(marks[i]))
                    {
                        response.error = true;
                        response.errorMessage = "Please provide a module name for all marks entered";
                        break;
                    }
                }
                
                // If not, see if there is a valid mark
            }
        }


        return response;
    }


    private bool IsMarkValidInteger(string mark)
    {
        int intMark;
        if (int.TryParse(mark, out intMark))
        {
            return (intMark <= 100 && intMark >= 0);
        }

        return false;
    }

    private bool IsStringValidModule(string module) => !string.IsNullOrWhiteSpace(module);

}

