const app = require("../../src/app");
const supertest = require("supertest");
const { expect } = require("chai");
const should = require("chai").should();

const api = supertest(app);

describe("Testing the health check API", () => {
    test("Calling the health check API returns 200", async () => {
        const { body } = await api
            .get("/health")
            .expect(200)
            .expect("Content-Type", /application\/json/);

        body.should.have.property("uptime");
        body.should.have.property("message").equals("Ok");
        body.should.have.property("date");
    });
});
