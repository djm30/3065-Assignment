const { assert } = require("chai");
const should = require("chai").should();

const marksForNext = require("../../src/marksForNext");

describe("Should calculate the marks required to get the next grade", () => {
    test("Average over 70 should show a first", () => {
        const marks = [71, 71, 71, 71, 71];

        const result = marksForNext(marks);

        expect(result).toBe("You are already sitting on a First!");
    });

    test("Average between 60 & 70 should return appropiate amount of marks needed for a first", () => {
        const marks = [61, 61, 61, 61, 61];

        const result = marksForNext(marks);

        expect(result).toBe(
            "Your current overall mark is: 61.0, and is 9.0 marks from a First, you need a total of 45.0 more marks across all 5 possible modules to reach this.",
        );
    });

    test("Average between 50 & 60 should return appropiate amount of marks needed for a 2.1", () => {
        const marks = [51, 51, 51, 51, 51];

        const result = marksForNext(marks);

        expect(result).toBe(
            "Your current overall mark is: 51.0, and is 9.0 marks from a 2.1, you need a total of 45.0 more marks across all 5 possible modules to reach this.",
        );
    });

    test("Average between 40 & 50 should return appropiate amount of marks needed for a 2.2", () => {
        const marks = [41, 41, 41, 41, 41];

        const result = marksForNext(marks);

        expect(result).toBe(
            "Your current overall mark is: 41.0, and is 9.0 marks from a 2.2, you need a total of 45.0 more marks across all 5 possible modules to reach this.",
        );
    });

    test("Average below 40 should return appropiate amount of marks needed for a third", () => {
        const marks = [31, 31, 31, 31, 31];

        const result = marksForNext(marks);

        expect(result).toBe(
            "Your current overall mark is: 31.0, and is 9.0 marks from a Third, you need a total of 45.0 more marks across all 5 possible modules to reach this.",
        );
    });

    test("Average of 0 should return appropiate amount of marks needed for a third", () => {
        const marks = [0, 0, 0, 0, 0];

        const result = marksForNext(marks);

        expect(result).toBe(
            "Your current overall mark is: 0.0, and is 40.0 marks from a Third, you need a total of 200.0 more marks across all 5 possible modules to reach this.",
        );
    });

    test("Works with undefined values in the marks array", () => {
        const marks = [80, 80, undefined, 80, 80];

        const result = marksForNext(marks);

        expect(result).toBe(
            "Your current overall mark is: 64.0, and is 6.0 marks from a First, you need a total of 30.0 more marks across all 5 possible modules to reach this.",
        );
    });
});
