import fetch from "./fetch";
import { Kinds, TotalMarksResponse } from "../types";
import { ServiceURLS } from "./service_urls";

export default async (
    modules: string[],
    marks: number[],
): Promise<TotalMarksResponse> => {
    const baseUrl = ServiceURLS.getInstance().urls.total;
    const response = await fetch<TotalMarksResponse>(baseUrl, modules, marks);
    response.kind = Kinds.total;
    return response;
};
