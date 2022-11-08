from src.overall_mark import overall_mark, marks_to_int

class Test_marks_to_int():
    def test_marks_to_int_works_with_string_array(self):
        result = marks_to_int(["50", "50", "50", "50", "50"])
        assert list(result) == [50,50,50,50,50]

    def test_marks_to_int_works_with_int_array(self):
        result = marks_to_int([50, 50, 50, 50, 50])
        assert list(result) == [50,50,50,50,50]

    def test_marks_to_int_works_with_None_array(self):
        result = marks_to_int([None, None, None, None, None])
        assert list(result) == [0,0,0,0,0]

    def test_marks_to_int_works_with_mixed_array(self):
        result = marks_to_int(["20", None, None, None, 30])
        assert list(result) == [20,0,0,0,30]

class Test_overall_mark():
    def test_returns_70_with_strings(self):
        result = overall_mark(["70", "70", "70", "70", "70"])
        assert result == 70

    def test_returns_70_with_ints(self):
        result = overall_mark([70, 70, 70, 70, 70])
        assert result == 70

    def test_returns_60(self):
        result = overall_mark(["60", "60", "60", "60", "60"])
        assert result == 60

    def test_returns_50(self):
        result = overall_mark(["50", "50", "50", "50", "50"])
        assert result == 50

    def test_returns_40(self):
        result = overall_mark(["40", "40", "40", "40", "40"])
        assert result == 40

    def test_returns_0(self):
        result = overall_mark(["0", "0", "0", "0", "0"])
        assert result == 0

    def test_returns_x_with_None_values(self):
        result = overall_mark(["0", "0", "0", None, "0"])
        assert result == 0