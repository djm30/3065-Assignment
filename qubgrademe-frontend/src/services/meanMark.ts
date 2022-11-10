import fetch from "./fetch";
import { Kinds, MeanMarkResponse } from "../types";
import { ServiceURLS } from "./service_urls";

export default async (
    modules: string[],
    marks: number[],
): Promise<MeanMarkResponse> => {
    const baseUrl = ServiceURLS.getInstance().urls.mean;
    const response = await fetch<MeanMarkResponse>(baseUrl, modules, marks);
    response.kind = Kinds.mean;
    return response;
};
