package main;

import org.junit.jupiter.api.Test;
import org.springframework.boot.test.context.SpringBootTest;
import validation.Validator;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

import static org.assertj.core.api.AssertionsForClassTypes.assertThat;

@SpringBootTest
public class ValidatorTests {

    // IsEmpty
    @Test
    void Is_Empty_Returns_True_For_All_Empty(){
        assertThat(Validator.allEmpty(Arrays.asList("","","","",""))).isTrue();
    }

    @Test
    void Is_Empty_Returns_True_For_All_Whitespace_Empty(){
        assertThat(Validator.allEmpty(Arrays.asList("  ","  "," ","   ","  "))).isTrue();
    }

    @Test
    void Is_Empty_Returns_False_When_Not_All_Empty(){
        assertThat(Validator.allEmpty(Arrays.asList("notempty","  "," ","   ","  "))).isFalse();
    }

    // ValidateInt
    @Test
    void Zero_Is_Valid_Integer(){
        assertThat(Validator.validateInt("0")).isTrue();
    }

    @Test
    void One_Hundred_Is_Valid_Integer(){
        assertThat(Validator.validateInt("100")).isTrue();
    }

    @Test
    void One_Hundred_One_Is_Invalid_Integer(){
        assertThat(Validator.validateInt("101")).isFalse();
    }

    @Test
    void Minus_One_Is_Invalid_Integer(){
        assertThat(Validator.validateInt("-1")).isFalse();
    }

    @Test
    void Word_Not_Valid_Integer(){
        assertThat(Validator.validateInt("word")).isFalse();
    }


    // Marks to int
    @Test
    void Marks_To_Int_Returns_Expected(){
        assertThat(Validator
                .marksToInteger(Arrays.asList("10","20","30","40","50")))
                .asList()
                .isEqualTo(Arrays.asList(10,20,30,40,50));
    }

    @Test
    void Marks_To_Int_Replaces_Invalid_With_0(){
        assertThat(Validator
                .marksToInteger(Arrays.asList("101","sdafs","0","40","-1")))
                .asList()
                .isEqualTo(Arrays.asList(0,0,0,40,0));
    }

    // Validate

    @Test
    void Valid_Values_Are_Validated(){
        List<String> modules = new ArrayList<String>(Arrays.asList("One","Two","Three","Four","Five"));
        List<String> marks = new ArrayList<String>(Arrays.asList("10","20","30","40","50"));

        assertThat(Validator.validate(modules, marks)).isEqualTo("");
    }

    @Test
    void All_Modules_Empty_Not_Valid(){
        List<String> modules = new ArrayList<String>(Arrays.asList("","","","",""));
        List<String> marks = new ArrayList<String>(Arrays.asList("10","20","30","40","50"));

        assertThat(Validator.validate(modules, marks)).isEqualTo("Please provide some module codes and their respective marks");
    }

    @Test
    void Module_Missing_A_Mark_Is_Not_Valid(){
        List<String> modules = new ArrayList<String>(Arrays.asList("One","Two","Three","Four","Five"));
        List<String> marks = new ArrayList<String>(Arrays.asList("10","20","30","40",""));

        assertThat(Validator.validate(modules, marks)).isEqualTo("Please provide a valid integer for every entered module");
    }

    @Test
    void Mark_Missing_A_Module_Is_Not_Valid(){
        List<String> modules = new ArrayList<String>(Arrays.asList("One","Two","Three","Four",""));
        List<String> marks = new ArrayList<String>(Arrays.asList("10","20","30","40","50"));

        assertThat(Validator.validate(modules, marks)).isEqualTo("Please provide a module name for all marks entered");
    }

    @Test
    void Missing_Pair_Of_Mark_And_Module_Is_Valid(){
        List<String> modules = new ArrayList<String>(Arrays.asList("One","Two","Three","Four",""));
        List<String> marks = new ArrayList<String>(Arrays.asList("10","20","30","40",""));

        assertThat(Validator.validate(modules, marks)).isEqualTo("");
    }

    @Test
    void Two_Missing_Pairs_Of_Mark_And_Module_Is_Valid(){
        List<String> modules = new ArrayList<String>(Arrays.asList("One","Two","Three","",""));
        List<String> marks = new ArrayList<String>(Arrays.asList("10","20","30","",""));

        assertThat(Validator.validate(modules, marks)).isEqualTo("");
    }

    @Test
    void Three_Missing_Pairs_Of_Mark_And_Module_Is_Valid(){
        List<String> modules = new ArrayList<String>(Arrays.asList("One","Two","","",""));
        List<String> marks = new ArrayList<String>(Arrays.asList("10","20","","",""));

        assertThat(Validator.validate(modules, marks)).isEqualTo("");
    }

    @Test
    void Four_Missing_Pairs_Of_Mark_And_Module_Is_Valid(){
        List<String> modules = new ArrayList<String>(Arrays.asList("One","","","",""));
        List<String> marks = new ArrayList<String>(Arrays.asList("10","","","",""));

        assertThat(Validator.validate(modules, marks)).isEqualTo("");
    }
}
