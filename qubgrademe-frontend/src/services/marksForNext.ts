import fetch from "./fetch";
import { Kinds, MarksForNextResponse } from "../types";
import { ServiceURLS } from "./service_urls";

export default async (
    modules: string[],
    marks: number[],
): Promise<MarksForNextResponse> => {
    const baseUrl = ServiceURLS.getInstance().urls.next;
    const response = await fetch<MarksForNextResponse>(baseUrl, modules, marks);
    response.kind = Kinds.marksForNext;
    return response;
};
