import fetch from "./fetch";
import { Kinds, MinMaxResponse } from "../types";
import { ServiceURLS } from "./service_urls";

export default async (
    modules: string[],
    marks: number[],
): Promise<MinMaxResponse> => {
    const services = ServiceURLS.getInstance();
    const url = services.GetProxy() + services.routes.maxmin;
    const response = await fetch<MinMaxResponse>(url, modules, marks);
    response.kind = Kinds.minMax;
    return response;
};
