module.exports = convertAndRemoveNullMarksModules = (response) => {
    // Replacing null values in modules and marks to 0 or ""
    response.marks = response.marks.map((x) =>
        Number.isNaN(parseInt(x)) ? 0 : parseInt(x),
    );

    response.modules = response.modules.map((x) =>
        x === undefined || x === null ? "" : x,
    );
};
