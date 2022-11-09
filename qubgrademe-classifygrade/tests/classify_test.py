from src.classify import classify



class Test_classify():
    def test_average_over_70_returns_first(self):
        result = classify(75)
        assert result.value == "First"

    def test_average_of_70_returns_first(self):
        result = classify(70)
        assert result.value == "First"

    def test_average_over_60_below_70_returns_twoone(self):
        result = classify(69)
        assert result.value == "2.1"

    def test_average_of_60_returns_first(self):
        result = classify(60)
        assert result.value == "2.1"

    def test_average_over_50_below_60_returns_twoone(self):
        result = classify(59)
        assert result.value == "2.2"

    def test_average_of_50_returns_first(self):
        result = classify(50)
        assert result.value == "2.2"

    def test_average_over_40_below_50_returns_twoone(self):
        result = classify(49)
        assert result.value == "Third"

    def test_average_of_40_returns_first(self):
        result = classify(40)
        assert result.value == "Third"

    def test_average_below_40_returns_fail(self):
        result = classify(39)
        assert result.value == "Fail"
    
    def test_average_of_0_returns_fail(self):
        result = classify(0)
        assert result.value == "Fail"