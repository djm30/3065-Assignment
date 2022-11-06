const validate = (modules, marks) => {
  let success = true;
  let message = "";

  // Checking if all modules are empty
  const areModulesEmpty = modules.reduce(
    (prev, curr) => (prev ? (curr ? curr.trim().length === 0 : false) : prev),
    true,
  );
  if (areModulesEmpty) {
    success = false;
    message = "Please provide some module codes and their respective marks";
  }

  if (success) {
    for (let i = 0; i < 5; i++) {
      // Checking if there is a module at this index
      if (modules[i] && modules[i].trim().length > 0) {
        // If there is checking that there is a valid, corresponding integer for the mark
        if (!isStringPositiveInteger(marks[i])) {
          message = "Please provide a valid integer for every entered module";
          success = false;
          break;
        }
      }
      // If there isnt a module present at this index
      // but there is a valid mark
      if (isStringPositiveInteger(marks[i])) {
        message = "Please provide a module name for all marks entered";
        success = false;
      }
    }
  }

  return { success, message };
};

const isStringPositiveInteger = (string) => {
  const num = parseInt(string);
  if (!Number.isNaN(num)) {
    return num >= 0 && num <= 100;
  }
};

module.exports = validate;
