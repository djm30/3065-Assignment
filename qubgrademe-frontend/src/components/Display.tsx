import React, { useEffect } from "react";
import { Kinds, MinMaxResponse, SortedResponse } from "../types";

interface Props {
    result: MinMaxResponse | SortedResponse | undefined;
}

const getMinMaxString = (result: MinMaxResponse): string => {
    return `
  Highest:   ${result.max_module}
  Lowest:    ${result.min_module}
  `;
};

const getSortedString = (result: SortedResponse): string => {
    let s = "\n";
    result.sorted_modules.forEach((x) => {
        x.module ? (s += x.module + "-" + x.marks + "\n") : null;
    });
    return s;
};

const Display = ({ result }: Props) => {
    let displayString = "";
    console.log(result);

    switch (result?.kind) {
        case Kinds.minMax:
            displayString = getMinMaxString(result as MinMaxResponse);
            break;
        case Kinds.sort:
            displayString = getSortedString(result as SortedResponse);
            break;
        default:
            displayString = "";
            break;
    }

    console.log(displayString);
    return (
        <textarea
            className="w-4/5 p-4 text-center bg-neutral-900 rounded-lg focus:outline-none border-[0.1px] border-transparent hover:border-neutral-400  text-white transition-all"
            readOnly={true}
            rows={5}
            cols={35}
            placeholder="Results pending"
            value={displayString}
        ></textarea>
    );
};

export default Display;
