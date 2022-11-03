import React from "react";

interface Props {
  setValue: React.Dispatch<React.SetStateAction<number>>;
  value: number;
}

const ScoreInput = ({ setValue, value }: Props) => {
  return (
    <input
      type="number"
      value={value}
      onChange={(e) => setValue(parseInt(e.target.value))}
      placeholder="Module Name"
      className="bg-neutral-900 md:text-lg rounded-lg py-2 px-6 grow-0 w-1/4 focus:outline-none border-[0.1px] border-transparent hover:border-neutral-400 focus:border-neutral-600 text-white transition-all"
    />
  );
};

export default ScoreInput;
