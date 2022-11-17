import React, { useEffect } from "react";
import {
    ClassifyGradeResponse,
    Kinds,
    MarksForNextResponse,
    MeanMarkResponse,
    MinMaxResponse,
    SortedResponse,
    TotalMarksResponse,
} from "../types";

interface Props {
    result:
        | MinMaxResponse
        | SortedResponse
        | TotalMarksResponse
        | ClassifyGradeResponse
        | MarksForNextResponse
        | MeanMarkResponse
        | undefined;
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
        x.module ? (s += x.module + " - " + x.marks + "\n") : null;
    });
    return s;
};

const getTotalString = (result: TotalMarksResponse): string => {
    return `\nTotal Marks: ${result.total}`;
};

const getMarksForNextString = (result: MarksForNextResponse): string => {
    return "\n" + result.marks_required;
};

const getClassificationString = (result: ClassifyGradeResponse): string => {
    return `\nCurrent Grade: ${result.grade}`;
};

const getMeanString = (result: MeanMarkResponse): string => {
    return `\nAverage Mark: ${result.mean}`;
};

const Display = ({ result }: Props) => {
    let displayString = "";

    switch (result?.kind) {
        case Kinds.minMax:
            displayString = getMinMaxString(result as MinMaxResponse);
            break;
        case Kinds.sort:
            displayString = getSortedString(result as SortedResponse);
            break;
        case Kinds.total:
            displayString = getTotalString(result as TotalMarksResponse);
            break;
        case Kinds.marksForNext:
            displayString = getMarksForNextString(
                result as MarksForNextResponse,
            );
            break;
        case Kinds.classify:
            displayString = getClassificationString(
                result as ClassifyGradeResponse,
            );
            break;
        case Kinds.mean:
            displayString = getMeanString(result as MeanMarkResponse);
            break;

        default:
            displayString = "";
            break;
    }
    return (
        <textarea
            className="w-4/5 p-4 text-lg text-center bg-neutral-800 rounded-md focus:outline-none border-[0.1px] border-transparent hover:border-neutral-400  text-white transition-all"
            readOnly={true}
            rows={5}
            cols={35}
            placeholder="Results pending"
            value={displayString}
        ></textarea>
    );
};

export default Display;
