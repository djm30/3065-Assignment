import fetch from "./fetch";
import { Kinds, TotalMarksResponse } from "../types";
import { ServiceURLS } from "./service_urls";

export default async (
    modules: string[],
    marks: number[],
): Promise<TotalMarksResponse> => {
    const services = ServiceURLS.getInstance();
    const url = services.GetProxy() + services.routes.total;
    const response = await fetch<TotalMarksResponse>(url, modules, marks);
    response.kind = Kinds.total;
    return response;
};
