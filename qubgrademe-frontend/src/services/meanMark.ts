import fetch from "./fetch";
import { Kinds, MeanMarkResponse } from "../types";

const baseUrl =
    "https://meanmark-service-development.azurewebsites.net/api/mean";

export default async (
    modules: string[],
    marks: number[],
): Promise<MeanMarkResponse> => {
    const response = await fetch<MeanMarkResponse>(baseUrl, modules, marks);
    response.kind = Kinds.mean;
    return response;
};
