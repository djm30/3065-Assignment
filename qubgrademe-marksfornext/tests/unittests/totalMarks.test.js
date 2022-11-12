const total = require("../../src/totalMarks");

describe("Total should return the sum of the array", () => {
    test("Should be 88 for an array of numbers", () => {
        const result = total([22, 22, 22, 22, 0]);
        expect(result).toBe(88);
    });

    test("Should be 50 for an array of numbers", () => {
        const result = total([10, 10, 10, 10, 10]);
        expect(result).toBe(50);
    });

    test("Works with NaN values in the array", () => {
        const result = total([10, 10, 10, NaN, 10]);
        expect(result).toBe(40);
    });

    test("Works with undefined values", () => {
        const result = total([10, 10, 10, undefined, 10]);
        expect(result).toBe(40);
    });

    test("Works with null values", () => {
        const result = total([10, 10, 10, null, 10]);
        expect(result).toBe(40);
    });

    test("All NaN values will return 0", () => {
        const result = total([NaN, NaN, NaN, NaN, NaN]);
        expect(result).toBe(0);
    });
});
