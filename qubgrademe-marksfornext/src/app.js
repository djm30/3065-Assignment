const express = require("express");
const cors = require("cors");
const { validate } = require("./validate");
const marksForNext = require("./marksForNext");
const convertAndRemoveNullMarksModules = require("./removeIncorrect");
const { format } = require("date-fns");

const app = express();

app.use(cors());

// app.get("/marks", (req, res) => {
//     const {
//         module_1,
//         module_2,
//         module_3,
//         module_4,
//         module_5,
//         mark_1,
//         mark_2,
//         mark_3,
//         mark_4,
//         mark_5,
//     } = req.query;

//     const modules = [module_1, module_2, module_3, module_4, module_5];
//     modules.forEach((x) => {
//         if (x === undefined) x = "";
//     });

//     const marks = [mark_1, mark_2, mark_3, mark_4, mark_5];

//     let response = {
//         error: false,
//         errorMessage: "",
//         modules: modules,
//         marks: marks,
//         total: -1,
//     };

//     const { success, message } = validate(modules, marks);

//     if (success) {
//         const totalMarks = total(marks.map((x) => parseInt(x)));
//         response = { ...response, total: totalMarks };
//     } else {
//         response = { ...response, error: true, errorMessage: message };
//     }

//     convertAndRemoveNullMarksModules(response);

//     return success
//         ? res.status(200).json(response)
//         : res.status(400).json(response);
// });

app.get("/", (req, res) => {
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
        marks_required: "",
    };

    const { success, message } = validate(modules, marks);

    if (success) {
        const marks_required = marksForNext(marks.map((x) => parseInt(x)));
        response = { ...response, marks_required };
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
        date: format(new Date(), "yyyy-MM-dd HH:mm:ss.SSSS"),
        message: "Ok",
        uptime: process.uptime(),
    };
    res.status(200).json(data);
});

module.exports = app;
