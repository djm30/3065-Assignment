import React from "react";

interface Props {
  setValue: React.Dispatch<React.SetStateAction<string>>;
  value: string;
}

const NameInput = ({ setValue, value }: Props) => {
  return (
    <input
      type="text"
      value={value}
      onChange={(e) => setValue(e.target.value)}
      placeholder="Module Name"
      className="bg-neutral-900 md:text-lg  rounded-lg py-2 px-6 grow-[4] focus:outline-none border-[0.1px] border-transparent hover:border-neutral-400 focus:border-neutral-600 text-white transition-all"
    />
  );
};

export default NameInput;
