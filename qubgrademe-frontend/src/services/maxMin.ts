import fetch from "./fetch";
import { Kinds, MinMaxResponse } from "../types";

const baseUrl = "http://localhost:9002";

export default async (
    modules: string[],
    marks: number[],
): Promise<MinMaxResponse> => {
    const response = await fetch<MinMaxResponse>(baseUrl, modules, marks);
    response.kind = Kinds.minMax;
    return response;
};
