import fetch from "./fetch";
import { Kinds, TotalMarksResponse } from "../types";

const baseUrl = "http://localhost:9003/marks";

export default async (
    modules: string[],
    marks: number[],
): Promise<TotalMarksResponse> => {
    const response = await fetch<TotalMarksResponse>(baseUrl, modules, marks);
    response.kind = Kinds.total;
    return response;
};
