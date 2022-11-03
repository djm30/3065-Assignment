import React from "react";
import NameInput from "./NameInput";
import ScoreInput from "./ScoreInput";

const Module = () => {
  return (
    <div className="flex w-4/5 justify-between gap-10">
      <NameInput />
      <ScoreInput />
    </div>
  );
};

export default Module;
