import fetch from "./fetch";
import { Kinds, MeanMarkResponse } from "../types";
import { ServiceURLS } from "./service_urls";

export default async (
    modules: string[],
    marks: number[],
): Promise<MeanMarkResponse> => {
    const services = ServiceURLS.getInstance();
    const url = services.GetProxy() + services.routes.mean;
    const response = await fetch<MeanMarkResponse>(url, modules, marks);
    response.kind = Kinds.mean;
    return response;
};
