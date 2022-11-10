const totalMarks = require("./totalMarks");

const marksForNext = (marks) => {
    const currentAverage = totalMarks(marks) / 5;

    if (currentAverage >= 70) {
        return "You are already sitting on a First!";
    }
    if (currentAverage < 70 && currentAverage >= 60) {
        return getMessage(70, currentAverage, "First");
    }
    if (currentAverage < 60 && currentAverage >= 50) {
        return getMessage(60, currentAverage, "2.1");
    }
    if (currentAverage < 50 && currentAverage >= 40) {
        return getMessage(50, currentAverage, "2.2");
    }
    if (currentAverage < 40) {
        return getMessage(40, currentAverage, "Third");
    }
};

const getMessage = (upperBound, currentAverage, grade) =>
    `Your current overall mark is: ${currentAverage.toFixed(1)}, and is ${(
        upperBound - currentAverage
    ).toFixed(1)} marks from a ${grade}, you need a total of ${(
        (upperBound - currentAverage) *
        5
    ).toFixed(2)} more marks across all 5 possible modules to reach this.`;

module.exports = marksForNext;
