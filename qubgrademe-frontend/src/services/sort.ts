import fetch from "./fetch";

import { SortedResponse, Kinds } from "../types";
const baseUrl = "http://localhost:9001";

export default async (
    modules: string[],
    marks: number[],
): Promise<SortedResponse> => {
    const response = await fetch<SortedResponse>(baseUrl, modules, marks);
    response.kind = Kinds.sort;
    return response;
};
