const express = require("express");
const total = require("./totalMarks");
const { validate } = require("./validate");
const marksForNext = require("./marksForNext");
const convertAndRemoveNullMarksModules = require("./removeIncorrect");

const app = express();

app.get("/marks", (req, res) => {
    const {
        module_1,
        module_2,
        module_3,
        module_4,
        module_5,
        mark_1,
        mark_2,
        mark_3,
        mark_4,
        mark_5,
    } = req.query;

    const modules = [module_1, module_2, module_3, module_4, module_5];
    modules.forEach((x) => {
        if (x === undefined) x = "";
    });

    const marks = [mark_1, mark_2, mark_3, mark_4, mark_5];

    let response = {
        error: false,
        errorMessage: "",
        modules: modules,
        marks: marks,
        total: -1,
    };

    const { success, message } = validate(modules, marks);

    if (success) {
        const totalMarks = total(marks.map((x) => parseInt(x)));
        response = { ...response, total: totalMarks };
    } else {
        response = { ...response, error: true, errorMessage: message };
    }

    convertAndRemoveNullMarksModules(response);

    return success
        ? res.status(200).json(response)
        : res.status(400).json(response);
});

app.get("/next", (req, res) => {
    const {
        module_1,
        module_2,
        module_3,
        module_4,
        module_5,
        mark_1,
        mark_2,
        mark_3,
        mark_4,
        mark_5,
    } = req.query;

    const modules = [module_1, module_2, module_3, module_4, module_5];
    modules.forEach((x) => {
        if (x === undefined) x = "";
    });

    const marks = [mark_1, mark_2, mark_3, mark_4, mark_5];

    let response = {
        error: false,
        errorMessage: "",
        modules: modules,
        marks: marks,
        marksRequired: "",
    };

    const { success, message } = validate(modules, marks);

    if (success) {
        const marksRequired = marksForNext(marks.map((x) => parseInt(x)));
        response = { ...response, marksRequired };
    } else {
        response = { ...response, error: true, errorMessage: message };
    }

    convertAndRemoveNullMarksModules(response);

    return success
        ? res.status(200).json(response)
        : res.status(400).json(response);
});

app.get("/health", (req, res) => {
    const data = {
        uptime: process.uptime(),
        message: "Ok",
        date: new Date(),
    };
    res.status(200).json(data);
});

module.exports = app;
