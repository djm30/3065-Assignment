const { assert, expect } = require("chai");
const should = require("chai").should();
const convertAndRemoveNullMarksModules = require("../../src/removeIncorrect");

describe("Should remove incorrect string and int values from the modules and marks arrays of a response object", () => {
    test("Replaces undefined modules with empty strings", () => {
        // Marks are already integers
        const marks = [10, 40, 30, 100, 21];
        // Modules has one undefined module
        const modules = [
            "CSC 3021",
            "CSC 3059",
            "CSC 3063",
            "CSC 3065",
            undefined,
        ];

        const response = {
            marks: [...marks],
            modules: [...modules],
        };
        convertAndRemoveNullMarksModules(response);

        expect(response.modules).to.have.same.members([
            "CSC 3021",
            "CSC 3059",
            "CSC 3063",
            "CSC 3065",
            "",
        ]);
        expect(response.marks).to.have.same.members(marks);
    });
    test("Replaces undefined marks with 0 and converts string number to integers", () => {
        const marks = ["10", "40", "30", "100", undefined];
        // Modules has one undefined module
        const modules = [
            "CSC 3021",
            "CSC 3059",
            "CSC 3063",
            "CSC 3065",
            "CSC 3068",
        ];

        const response = {
            marks: [...marks],
            modules: [...modules],
        };
        convertAndRemoveNullMarksModules(response);

        expect(response.modules).to.have.same.members(modules);
        expect(response.marks).to.have.same.members([10, 40, 30, 100, 0]);
    });
    test("Replaces both undefined modules and undefined marks", () => {
        const marks = ["10", "40", "30", "100", undefined];
        // Modules has one undefined module
        const modules = [
            "CSC 3021",
            "CSC 3059",
            "CSC 3063",
            "CSC 3065",
            undefined,
        ];

        const response = {
            marks: [...marks],
            modules: [...modules],
        };
        convertAndRemoveNullMarksModules(response);

        expect(response.modules).to.have.same.members([
            "CSC 3021",
            "CSC 3059",
            "CSC 3063",
            "CSC 3065",
            "",
        ]);
        expect(response.marks).to.have.same.members([10, 40, 30, 100, 0]);
    });
});
