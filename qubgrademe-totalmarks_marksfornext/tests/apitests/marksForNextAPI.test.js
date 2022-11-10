const app = require("../../src/app");
const supertest = require("supertest");
const { expect } = require("chai");
const should = require("chai").should();

const api = supertest(app);

describe("Testing the marks for next endpoint", () => {
    test("Returns 200 OK and appropiate response for valid request", async () => {
        const { body } = await api
            .get(
                "/next?module_1=One&module_2=Two&module_3=Three&module_4=Four&module_5=Five&mark_1=65&mark_2=65&mark_3=65&mark_4=65&mark_5=65",
            )
            .expect(200)
            .expect("Content-Type", /application\/json/);

        body.should.have.property("error").is.false;
        body.should.have.property("errorMessage").equals("");
        body.should.have
            .property("modules")
            .has.same.members(["One", "Two", "Three", "Four", "Five"]);
        body.should.have
            .property("marks")
            .has.same.members([65, 65, 65, 65, 65]);
        body.should.have
            .property("marks_required")
            .equals(
                "Your current average is: 65, and is 5 marks from a First, you need a total of 25 more marks across all 5 possible modules to reach this.",
            );
    });
    test("Returns 400 Bad Request and appropiate response for valid request", async () => {
        const { body } = await api
            .get(
                "/next?&module_2=Two&module_3=Three&module_4=Four&module_5=Five&mark_1=65&mark_2=65&mark_3=65&mark_4=65&mark_5=65",
            )
            .expect(400)
            .expect("Content-Type", /application\/json/);

        body.should.have.property("error").is.true;
        body.should.have
            .property("errorMessage")
            .equals("Please provide a module name for all marks entered");
        body.should.have
            .property("modules")
            .has.same.members(["", "Two", "Three", "Four", "Five"]);
        body.should.have
            .property("marks")
            .has.same.members([65, 65, 65, 65, 65]);
        body.should.have.property("marks_required").equals("");
    });
});
