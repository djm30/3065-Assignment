import React from "react";

interface Props {
    setValue: React.Dispatch<React.SetStateAction<number>>;
    value: number;
}

const ScoreInput = ({ setValue, value }: Props) => {
    return (
        <input
            type="number"
            value={Number.isNaN(value) ? "" : value}
            // @ts-ignore
            onChange={(e) => setValue(parseInt(e.target.value))}
            min={0}
            max={100}
            placeholder="Mark"
            className="bg-neutral-800 md:text-lg rounded-md py-2 px-6 grow-0 w-1/6 focus:outline-none border-[0.1px] border-transparent hover:border-neutral-400 focus:border-neutral-600 text-white transition-all"
        />
    );
};

export default ScoreInput;
