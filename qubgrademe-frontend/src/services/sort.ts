import axios from "axios";
import { SortedResponse, Kinds } from "../types";
import { getQueryString } from "./axios"

const baseUrl = "http://localhost:9001";


export default async (
    module1: string, module2: string, module3: string, module4: string, module5: string,
    mark1: number, mark2: number, mark3: number, mark4: number, mark5: number): Promise<SortedResponse> => {

    const { data } = await axios.get<SortedResponse>(`${baseUrl}/${getQueryString(module1, module2, module3, module4, module5, mark1, mark2, mark3, mark4, mark5)}`)
    data.kind = Kinds.sort;
    return data;

}