package main;

import org.junit.jupiter.api.Test;
import org.springframework.boot.test.context.SpringBootTest;
import services.GetGrade;

import java.util.ArrayList;
import java.util.Arrays;

import static org.assertj.core.api.AssertionsForClassTypes.assertThat;

@SpringBootTest
public class GetGradeTests {

    @Test
    void Get_Total_Returns_Expected_Amount(){
        assertThat(GetGrade.getTotal(Arrays.asList(10,10,10,10,10))).isEqualTo(50);
    }

    @Test
    void Returns_First_When_Overall_70_Or_Over(){
        String result = GetGrade.getGrade(Arrays.asList(70,70,70,70,70));
        assertThat(result).isEqualTo("First");
    }

    @Test
    void Returns_TwoOne_When_Overall_60_Or_Over(){
        String result = GetGrade.getGrade(Arrays.asList(60,60,60,60,60));
        assertThat(result).isEqualTo("2.1");
    }

    @Test
    void Returns_TwoTwo_When_Overall_50_Or_Over(){
        String result = GetGrade.getGrade(Arrays.asList(50,50,50,50,50));
        assertThat(result).isEqualTo("2.2");
    }

    @Test
    void Returns_Third_When_Overall_40_Or_Over(){
        String result = GetGrade.getGrade(Arrays.asList(40,40,40,40,40));
        assertThat(result).isEqualTo("Third");
    }

    @Test
    void Returns_Fail_When_Overall_39_Or_Lower(){
        String result = GetGrade.getGrade(Arrays.asList(39,39,39,39,39));
        assertThat(result).isEqualTo("Fail");
    }

    @Test
    void Returns_Fail_When_Overall_Is_0(){
        String result = GetGrade.getGrade(Arrays.asList(0,0,0,0,0));
        assertThat(result).isEqualTo("Fail");
    }
}
