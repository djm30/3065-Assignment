import fetch from "./fetch";
import { ClassifyGradeResponse, Kinds } from "../types";
import { ServiceURLS } from "./service_urls";

export default async (
    modules: string[],
    marks: number[],
): Promise<ClassifyGradeResponse> => {
    const baseUrl = ServiceURLS.getInstance().urls.classify;
    const response = await fetch<ClassifyGradeResponse>(
        baseUrl,
        modules,
        marks,
    );
    response.kind = Kinds.classify;
    return response;
};
