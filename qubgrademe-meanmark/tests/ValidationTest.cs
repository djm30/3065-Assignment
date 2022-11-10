namespace tests;
using Assignment2.MeanMark.Services;

public class ValidationTest
{

    private readonly Validator _validator;

    public ValidationTest()
    {
        _validator = new Validator();
    }

    [Fact]
    public void Passes_With_Valid_Values()
    {
        var modules = new List<string>() {"CSC 3021", "CSC 3059", "CSC 3063", "CSC 3065", "CSC 3068" };
        var marks = new List<string>() {"60", "70", "60", "70", "60" };

        var result = _validator.Validate(modules, marks);
        
        Assert.False(result.error);
        Assert.Equal("", result.errorMessage);
    }
    
    [Fact]
    public void Passes_With_Missing_Pair()
    {
        var modules = new List<string>() {"CSC 3021", "CSC 3059", "CSC 3063", "CSC 3065", "" };
        var marks = new List<string>() {"60", "70", "60", "70", "" };

        var result = _validator.Validate(modules, marks);
        
        Assert.False(result.error);
        Assert.Equal("", result.errorMessage);
    }
    
    [Fact]
    public void Passes_With_Two_Missing_Pairs()
    {
        var modules = new List<string>() {"CSC 3021", "CSC 3059", "CSC 3063", "", "" };
        var marks = new List<string>() {"60", "70", "60", "", "" };

        var result = _validator.Validate(modules, marks);
        
        Assert.False(result.error);
        Assert.Equal("", result.errorMessage);
    }
    
    [Fact]
    public void Passes_When_A_Mark_Is_100()
    {
        var modules = new List<string>() {"CSC 3021", "CSC 3059", "CSC 3063", "CSC 3065", "CSC 3068" };
        var marks = new List<string>() {"60", "70", "60", "70", "100" };

        var result = _validator.Validate(modules, marks);
        
        Assert.False(result.error);
        Assert.Equal("", result.errorMessage);
    }
    
    [Fact]
    public void Passes_When_A_Mark_Is_0()
    {
        var modules = new List<string>() {"CSC 3021", "CSC 3059", "CSC 3063", "CSC 3065", "CSC 3068" };
        var marks = new List<string>() {"60", "70", "60", "70", "0" };

        var result = _validator.Validate(modules, marks);
        
        Assert.False(result.error);
        Assert.Equal("", result.errorMessage);
    }
    
    [Fact]
    public void Fails_When_A_Module_Is_Missing_A_Mark()
    {
        var modules = new List<string>() {"CSC 3021", "CSC 3059", "CSC 3063", "CSC 3065", "CSC 3068" };
        var marks = new List<string>() {"60", "70", "60", "70", "" };

        var result = _validator.Validate(modules, marks);
        
        Assert.True(result.error);
        Assert.Equal("Please provide a valid integer for every entered module", result.errorMessage);
    }
    
    [Fact]
    public void Fails_When_A_Mark_Is_Missing_A_Module()
    {
        var modules = new List<string>() {"CSC 3021", "CSC 3059", "CSC 3063", "CSC 3065", "" };
        var marks = new List<string>() {"60", "70", "60", "70", "70" };

        var result = _validator.Validate(modules, marks);
        
        Assert.True(result.error);
        Assert.Equal("Please provide a module name for all marks entered", result.errorMessage);
    }
    
    [Fact]
    public void Fails_When_All_Modules_Are_Empty()
    {
        var modules = new List<string>() {"", "", "", "", "" };
        var marks = new List<string>() {"60", "70", "60", "70", "70" };

        var result = _validator.Validate(modules, marks);
        
        Assert.True(result.error);
        Assert.Equal("Please provide some module codes and their respective marks", result.errorMessage);
    }
    
    [Fact]
    public void Fails_When_All_Modules_Are_Whitespace()
    {
        var modules = new List<string>() {" ", "  ", "  ", "  ", "  " };
        var marks = new List<string>() {"60", "70", "60", "70", "70" };

        var result = _validator.Validate(modules, marks);
        
        Assert.True(result.error);
        Assert.Equal("Please provide some module codes and their respective marks", result.errorMessage);
    }
    
    [Fact]
    public void Fails_When_All_Modules_Are_Null()
    {
        var modules = new List<string>() {null, null, null, null, null };
        var marks = new List<string>() {"60", "70", "60", "70", "70" };

        var result = _validator.Validate(modules, marks);
        
        Assert.True(result.error);
        Assert.Equal("Please provide some module codes and their respective marks", result.errorMessage);
    }
    
    [Fact]
    public void Fails_When_A_Mark_Is_Over_100()
    {
        var modules = new List<string>() {"CSC 3021", "CSC 3059", "CSC 3063", "CSC 3065", "CSC 3068" };
        var marks = new List<string>() {"60", "70", "60", "70", "101" };

        var result = _validator.Validate(modules, marks);
        
        Assert.True(result.error);
        Assert.Equal("Please provide a valid integer for every entered module", result.errorMessage);
    }
    
    [Fact]
    public void Fails_When_A_Mark_Is_Below_0()
    {
        var modules = new List<string>() {"CSC 3021", "CSC 3059", "CSC 3063", "CSC 3065", "CSC 3068" };
        var marks = new List<string>() {"60", "70", "60", "70", "-1" };

        var result = _validator.Validate(modules, marks);
        
        Assert.True(result.error);
        Assert.Equal("Please provide a valid integer for every entered module", result.errorMessage);
    }

    [Fact] public void Fails_When_All_Marks_Are_Missing()
    {
        var modules = new List<string>() {"CSC 3021", "CSC 3059", "CSC 3063", "CSC 3065", "CSC 3068" };
        var marks = new List<string>() {"", "", "", "", "" };

        var result = _validator.Validate(modules, marks);
        
        Assert.True(result.error);
        Assert.Equal("Please provide a valid integer for every entered module", result.errorMessage);
    }
}