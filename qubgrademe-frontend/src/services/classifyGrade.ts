import fetch from "./fetch";
import { ClassifyGradeResponse, Kinds } from "../types";

const baseUrl = "http://localhost:9004/";

export default async (
    modules: string[],
    marks: number[],
): Promise<ClassifyGradeResponse> => {
    const response = await fetch<ClassifyGradeResponse>(
        baseUrl,
        modules,
        marks,
    );
    response.kind = Kinds.classify;
    return response;
};
