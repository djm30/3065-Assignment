import fetch from "./fetch";

import { SortedResponse, Kinds } from "../types";
import { ServiceURLS } from "./service_urls";

export default async (
    modules: string[],
    marks: number[],
): Promise<SortedResponse> => {
    const services = ServiceURLS.getInstance();
    const url = services.GetProxy() + services.routes.sort;
    const response = await fetch<SortedResponse>(url, modules, marks);
    response.kind = Kinds.sort;
    return response;
};
