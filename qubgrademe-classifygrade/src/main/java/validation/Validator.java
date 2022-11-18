package validation;

import java.util.ArrayList;
import java.util.List;

public class Validator {
    public static String validate(List<String> modules, List<String> marks){

        if(allEmpty(modules)){
            return "Please provide some module codes and their respective marks";
        }
        for(int i = 0; i < modules.size(); i++){
            // Check if there is a valid mark at this index
            if(modules.get(i).trim() != ""){
                // Check if there is not a valid integer at this point
                if(!validateInt(marks.get(i)))
                    return "Please provide a valid integer for every entered module";
            }else{
                // If there isnt a valid mark
                // Check if there is a valid integer
                if(validateInt(marks.get(i)))
                    return "Please provide a module name for all marks entered";
            }
        }
        return "";
    }

    public static boolean allEmpty(List<String> modules){
        return modules.size() == modules.stream().filter(m -> m.trim() == "").toList().size();
    }

    public static boolean validateInt(String mark){
        try{
            int num = Integer.parseInt(mark);
            if(num >= 0 && num <= 100)
                return true;
        }catch(NumberFormatException ignored){}
        return false;
    }

    public static List<Integer> marksToInteger(List<String> marks){
        return marks.stream().map(m -> validateInt(m) ? Integer.parseInt(m) : 0).toList();
    }
}
