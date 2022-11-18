package models;

import java.util.List;

public class ClassifyGradeModel {
    private boolean error;
    private String errorMessage;
    private List<String> modules;
    private List<Integer> marks;
    private String grade;

    public ClassifyGradeModel(){

    }

    public ClassifyGradeModel(boolean error, String errorMessage, List<String> modules, List<Integer> marks, String grade){
        setError(error);
        setErrorMessage(errorMessage);
        setModules(modules);
        setMarks(marks);
        setGrade(grade);
    }


    public boolean isError() {
        return error;
    }

    public void setError(boolean error) {
        this.error = error;
    }

    public String getErrorMessage() {
        return errorMessage;
    }

    public void setErrorMessage(String errorMessage) {
        this.errorMessage = errorMessage;
    }

    public List<String> getModules() {
        return modules;
    }

    public void setModules(List<String> modules) {
        this.modules = modules;
    }

    public List<Integer> getMarks() {
        return marks;
    }

    public void setMarks(List<Integer> marks) {
        this.marks = marks;
    }

    public String getGrade() {
        return grade;
    }

    public void setGrade(String grade) {
        this.grade = grade;
    }
}
