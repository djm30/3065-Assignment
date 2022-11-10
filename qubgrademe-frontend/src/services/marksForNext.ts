import fetch from "./fetch";
import { Kinds, MarksForNextResponse } from "../types";

const baseUrl = "http://localhost:9003/next";

export default async (
    modules: string[],
    marks: number[],
): Promise<MarksForNextResponse> => {
    const response = await fetch<MarksForNextResponse>(baseUrl, modules, marks);
    response.kind = Kinds.marksForNext;
    return response;
};
