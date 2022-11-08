const { validate, isStringPositiveInteger } = require("../../src/validate");
const { assert, expect } = require("chai");
const should = require("chai").should();

describe("Testing integer conversion", () => {
    test("'100' is a valid integer", () => {
        const result = isStringPositiveInteger("100");
        expect(result).to.be.true;
    });

    test("'0' is a valid integer", () => {
        const result = isStringPositiveInteger("0");
        expect(result).to.be.true;
    });

    test("'50' is a valid integer", () => {
        const result = isStringPositiveInteger("50");
        expect(result).to.be.true;
    });

    test("'101' is not valid integer", () => {
        const result = isStringPositiveInteger("101");
        expect(result).to.be.false;
    });

    test("'-1' is not valid integer", () => {
        const result = isStringPositiveInteger("-1");
        expect(result).to.be.false;
    });

    test("undefined is not valid integer", () => {
        const result = isStringPositiveInteger(undefined);
        expect(result).to.be.false;
    });

    test("null is not valid integer", () => {
        const result = isStringPositiveInteger(null);
        expect(result).to.be.false;
    });

    test("NaN is not valid integer", () => {
        const result = isStringPositiveInteger(NaN);
        expect(result).to.be.false;
    });
});

describe("Validates the query params to ensure a valid request has been sent", () => {
    test("Succeeds with full array of modules and marks", () => {
        const modules = [
            "CSC 3065",
            "CSC3021",
            "CSC3063",
            "CSC3059",
            "CSC3069",
        ];
        const marks = ["80", "70", "70", "80", "60"];

        const result = validate(modules, marks);
        result.should.have.property("success").equals(true);
        result.should.have.property("message").equals("");
    });

    test("Succeeds with modules and marks missing at the end", () => {
        const modules = [
            "CSC 3065",
            "CSC3021",
            "CSC3063",
            "CSC3059",
            undefined,
        ];

        const marks = ["80", "70", "70", "80", undefined];

        const result = validate(modules, marks);
        result.should.have.property("success").equals(true);
        result.should.have.property("message").equals("");
    });

    test("Succeeds with modules and marks missing at the start", () => {
        const modules = [
            undefined,
            "CSC3021",
            "CSC3063",
            "CSC3059",
            "CSC 3065",
        ];

        const marks = [undefined, "70", "70", "80", "90"];

        const result = validate(modules, marks);
        result.should.have.property("success").equals(true);
        result.should.have.property("message").equals("");
    });

    test("Fails when a module has a mark of over 100", () => {
        const modules = [
            "CSC 3068",
            "CSC3021",
            "CSC3063",
            "CSC3059",
            "CSC 3065",
        ];

        const marks = ["30", "70", "70", "80", "101"];

        const result = validate(modules, marks);
        result.should.have.property("success").equals(false);
        result.should.have
            .property("message")
            .equals("Please provide a valid integer for every entered module");
    });

    test("Fails when a module has a mark below 0", () => {
        const modules = [
            "CSC 3068",
            "CSC3021",
            "CSC3063",
            "CSC3059",
            "CSC 3065",
        ];

        const marks = ["30", "70", "70", "80", "-3"];

        const result = validate(modules, marks);
        result.should.have.property("success").equals(false);
        result.should.have
            .property("message")
            .equals("Please provide a valid integer for every entered module");
    });

    test("Fails when a mark is provided without a module name", () => {
        const modules = [
            undefined,
            "CSC3021",
            "CSC3063",
            "CSC3059",
            "CSC 3065",
        ];

        const marks = ["30", "70", "70", "80", "10"];

        const result = validate(modules, marks);
        result.should.have.property("success").equals(false);
        result.should.have
            .property("message")
            .equals("Please provide a module name for all marks entered");
    });

    test("Fails when all modules are empty", () => {
        const modules = [undefined, undefined, undefined, undefined, undefined];

        const marks = ["30", "70", "70", "80", "10"];

        const result = validate(modules, marks);
        result.should.have.property("success").equals(false);
        result.should.have
            .property("message")
            .equals(
                "Please provide some module codes and their respective marks",
            );
    });
});
