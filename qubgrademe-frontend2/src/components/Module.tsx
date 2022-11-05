import React from "react";
import NameInput from "./NameInput";
import ScoreInput from "./ScoreInput";

interface Props {
  setValue: React.Dispatch<React.SetStateAction<string>>;
  setMarkValue: React.Dispatch<React.SetStateAction<number>>;
  value: string;
  markValue: number;
}

const Module = ({ value, setValue, markValue, setMarkValue }: Props) => {
  return (
    <div className="flex w-4/5 justify-between gap-10">
      <NameInput setValue={setValue} value={value} />
      <ScoreInput setValue={setMarkValue} value={markValue} />
    </div>
  );
};

export default Module;
