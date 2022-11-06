const total = (marks) => {
  return marks.reduce(
    (prev, curr) => (!Number.isNaN(curr) ? curr + prev : prev),
    0,
  );
};

module.exports = total;
