using Assignment2.MeanMark.Services;

namespace tests;

public class MeanTest
{
    private readonly IMeanService _meanService;

    public MeanTest()
    {
        _meanService = new MeanService();
    }

    [Fact]
    public void Returns_Expected_Mean_For_5_Marks()
    {
        var marks = new List<string>() {"65", "65", "65", "65", "65" };
        var mean = _meanService.CalculateMean(marks);
        Assert.Equal(65.0, mean);
    }
    
    [Fact]
    public void Returns_Expected_Mean_For_4_Marks_And_One_Empty_String()
    {
        var marks = new List<string>() {"65", "65", "65", "65", "" };
        var mean = _meanService.CalculateMean(marks);
        Assert.Equal(65.0, mean);
    }
    
    [Fact]
    public void Returns_Expected_Mean_For_4_Marks_And_One_Empty_String_At_The_Start()
    {
        var marks = new List<string>() {"", "65", "65", "65", "65" };
        var mean = _meanService.CalculateMean(marks);
        Assert.Equal(65.0, mean);
    }
    
    [Fact]
    public void Returns_Expected_Mean_For_4_Marks_And_One_Whitespace_String()
    {
        var marks = new List<string>() {"65", "65", "65", "65", "  " };
        var mean = _meanService.CalculateMean(marks);
        Assert.Equal(65.0, mean);
    }
    
    [Fact]
    public void Returns_0_For_All_Empty_Strings()
    {
        var marks = new List<string>() {"", "", "", "", "" };
        var mean = _meanService.CalculateMean(marks);
        Assert.Equal(0.0, mean);
    }
}