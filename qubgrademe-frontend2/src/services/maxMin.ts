import axios from "axios";
import { getQueryString } from "./axios"
import { Kinds, MinMaxResponse } from "../types";

const baseUrl = "http://localhost:9002";


export default async (
    module1: string, module2: string, module3: string, module4: string, module5: string,
    mark1: number, mark2: number, mark3: number, mark4: number, mark5: number): Promise<MinMaxResponse> => {
    const { data } = await axios.get<MinMaxResponse>(`${baseUrl}/${getQueryString(module1, module2, module3, module4, module5, mark1, mark2, mark3, mark4, mark5)}`)
    data.kind = Kinds.minMax;
    return data;
}