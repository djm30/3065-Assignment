package main;

import models.ClassifyGradeModel;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import services.GetGrade;
import validation.Validator;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Optional;

@SpringBootApplication
@RestController
@CrossOrigin(origins = {"*"})
public class QubGradeMeClassifyApplication {

    @GetMapping("/")
    public ResponseEntity<ClassifyGradeModel> getClassification(
            @RequestParam Optional<String> module_1,
            @RequestParam Optional<String> module_2,
            @RequestParam Optional<String> module_3,
            @RequestParam Optional<String> module_4,
            @RequestParam Optional<String> module_5,
            @RequestParam Optional<String> mark_1,
            @RequestParam Optional<String> mark_2,
            @RequestParam Optional<String> mark_3,
            @RequestParam Optional<String> mark_4,
            @RequestParam Optional<String> mark_5
            ){

        ClassifyGradeModel response = new ClassifyGradeModel();


        List<Optional<String>> queryModules = new ArrayList<Optional<String>>(Arrays.asList(module_1, module_2, module_3, module_4, module_5));
        List<Optional<String>> queryMarks = new ArrayList<Optional<String>>(Arrays.asList(mark_1, mark_2, mark_3, mark_4, mark_5));


        List<String> modules = queryModules.stream().map(m -> m.isPresent() ? m.get() : "").toList();
        List<String> stringMarks = queryMarks.stream().map(m -> m.isPresent() ? m.get() : "").toList();
        List<Integer> marks = Validator.marksToInteger(stringMarks);

        response.setModules(modules);
        response.setMarks(marks);

        String validationMessage = Validator.validate(modules, stringMarks);

        if(validationMessage.equals("")){
            response.setError(false);
            response.setErrorMessage("");
            response.setGrade(GetGrade.getGrade(marks));

        }else{
            response.setError(true);
            response.setErrorMessage(validationMessage);
            response.setGrade("");
        }

        return ResponseEntity.status(response.isError() ? 400 : 200).body(response);
    }

    public static void main(String[] args) {
        SpringApplication.run(QubGradeMeClassifyApplication.class, args);
    }
}
