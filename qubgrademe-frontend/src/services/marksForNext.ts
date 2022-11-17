import fetch from "./fetch";
import { Kinds, MarksForNextResponse } from "../types";
import { ServiceURLS } from "./service_urls";

export default async (
    modules: string[],
    marks: number[],
): Promise<MarksForNextResponse> => {
    const services = ServiceURLS.getInstance();
    const url = services.GetProxy() + services.routes.next;
    const response = await fetch<MarksForNextResponse>(url, modules, marks);
    response.kind = Kinds.marksForNext;
    return response;
};
