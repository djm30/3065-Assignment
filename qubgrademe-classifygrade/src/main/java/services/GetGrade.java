package services;

import java.util.List;

public class GetGrade {
    public static String getGrade(List<Integer> marks){
        int overallMark = getTotal(marks) / 5;

        if (overallMark >= 70)
            return "First";
        else if (overallMark >= 60)
            return "2.1";
        else if (overallMark >= 50)
            return "2.2";
        else if (overallMark >= 40)
            return "Third";
        else
            return "Fail";
    }

    public static int getTotal(List<Integer> marks){
        return marks.stream().reduce(0, (a,b) -> a+b);
    }
}
