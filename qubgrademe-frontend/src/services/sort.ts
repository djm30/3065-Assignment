import fetch from "./fetch";

import { SortedResponse, Kinds } from "../types";
import { ServiceURLS } from "./service_urls";

export default async (
    modules: string[],
    marks: number[],
): Promise<SortedResponse> => {
    const baseUrl = ServiceURLS.getInstance().urls.sort;
    const response = await fetch<SortedResponse>(baseUrl, modules, marks);
    response.kind = Kinds.sort;
    return response;
};
