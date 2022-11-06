const express = require("express");
const total = require("./totalMarks");
const validate = require("./validate");
const marksForNext = require("./marksForNext");

const app = express();
const port = process.env.PORT || 9003;

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

  console.log(req.query);

  const modules = [module_1, module_2, module_3, module_4, module_5];
  modules.forEach((x) => {
    if (x === undefined) x = "";
  });

  const marks = [mark_1, mark_2, mark_3, mark_4, mark_5];

  const { success, message } = validate(modules, marks);

  const totalMarks = total(marks.map((x) => parseInt(x)));

  console.log(marks);

  const response = {
    error: false,
    modules: modules,
    marks: marks,
    total: totalMarks,
  };

  res.status(200).json(response);
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

  const { success, message } = validate(modules, marks);

  const marksRequired = marksForNext(marks.map((x) => parseInt(x)));

  const response = {
    error: false,
    modules: modules,
    marks: marks,
    marksRequired,
  };

  res.status(200).json(response);
});

app.listen(port, () => {
  console.log(`Listening on ${port}`);
});
