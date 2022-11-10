import { ToastContent } from "react-toastify";
import { toast } from "react-toastify";
import { MinMaxResponse, SortedResponse, Kinds, Kind } from "../types";

const useFetch = (
    setResult: React.Dispatch<
        React.SetStateAction<
            | MinMaxResponse
            | SortedResponse
            | TotalMarksResponse
            | ClassifyGradeResponse
            | MarksForNextResponse
            | MeanMarkResponse
            | undefined
        >
    >,
) => {
    return async <T extends Kind>(
        getFunction: (modules: string[], marks: number[]) => T,
        modules: string[],
        marks: number[],
        kind: Kinds,
    ) => {
        try {
            const result = await getFunction(modules, marks);
            result.kind = kind;
            setResult(result);
        } catch (e: any) {
            console.log(e);
            toast.error(e.message);
        }
    };
};
