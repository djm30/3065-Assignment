import fetch from "./fetch";
import { ClassifyGradeResponse, Kinds } from "../types";
import { ServiceURLS } from "./service_urls";

export default async (
    modules: string[],
    marks: number[],
): Promise<ClassifyGradeResponse> => {
    const services = ServiceURLS.getInstance();
    const url = services.GetProxy() + services.routes.classify;
    const response = await fetch<ClassifyGradeResponse>(url, modules, marks);
    response.kind = Kinds.classify;
    return response;
};
