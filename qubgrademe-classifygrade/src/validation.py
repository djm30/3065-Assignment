def validate(modules, marks):
    success = True
    message = ""

    # Check if all modules are empty
    areModulesEmpty = all(x is None or len(x.strip()) == 0 for x in modules)
    if areModulesEmpty:
        success = False
        message = "Please provide some module codes and their respective marks"


    if success:
        for i in range(0,len(modules)):
            # Checking if there is a value present for the current module
            if(modules[i] is not None and len(modules[i].strip()) > 0):
                # Checking if there is a valid mark present for this mark
                if not is_string_positive_integer(marks[i]):
                    success = False
                    message = "Please provide a valid integer for every entered module"
                    break
            else:
                # Checking if a mark has been entered when no module name has been entered
                if is_string_positive_integer(marks[i]):
                    success = False
                    message = "Please provide a module name for all marks entered"
                    break

    return (success, message)

def is_string_positive_integer(num: str) -> bool:
    if isinstance(num, int):
        if (num <= 100 and num >= 0): return True
        else: return False
    if num is not None and num.isdigit():
        if int(num) <= 100 and int(num) >= 0:
            return True
    return False
