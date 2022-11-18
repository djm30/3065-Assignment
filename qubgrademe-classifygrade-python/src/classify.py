from classifications import Classifications

def classify(overall_mark) -> Classifications:
    if overall_mark >= 70:
        return Classifications.FIRST
    elif overall_mark <= 70 and overall_mark >= 60:
        return Classifications.TWOONE
    elif overall_mark <= 60 and overall_mark >= 50:
        return Classifications.TWOTWO
    elif overall_mark <= 50 and overall_mark >= 40:
        return Classifications.THIRD
    else:
        return Classifications.FAIL

