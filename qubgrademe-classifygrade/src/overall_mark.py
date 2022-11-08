def overall_mark(marks: list) -> int:
    return sum(marks_to_int(marks))/len(marks)

def marks_to_int(marks: list) -> list:
    return map(lambda x: int(x) if isinstance(x, int) or x is not None else 0, marks)