import fetch from "./fetch";
import { Kinds, MinMaxResponse } from "../types";
import { ServiceURLS } from "./service_urls";

export default async (
    modules: string[],
    marks: number[],
): Promise<MinMaxResponse> => {
    const baseUrl = ServiceURLS.getInstance().urls.maxmin;
    const response = await fetch<MinMaxResponse>(baseUrl, modules, marks);
    response.kind = Kinds.minMax;
    return response;
};
