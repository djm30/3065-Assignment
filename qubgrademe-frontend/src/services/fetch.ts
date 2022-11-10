import axios from "axios";

const client = axios.create({
    // baseURL: "http://localhost:",
    timeout: 5000,
    // validateStatus: (status) => {
    //     if ((status >= 200 && status <= 300) || status === 400) return true;
    //     return false;
    // },
});

const getQueryString = (modules: string[], marks: number[]): string => {
    let s = "?";
    modules.forEach((x, i) => {
        s += `&module_${i + 1}=${x}`;
    });
    marks.forEach((x, i) => {
        s += `&mark_${i + 1}=${Number.isNaN(x) ? "" : x}`;
    });
    return s;
};

const fetch = async <T>(
    url: string,
    modules: string[],
    marks: number[],
): Promise<T> => {
    const { data, status } = await client.get<T>(
        url + getQueryString(modules, marks),
    );
    return data;
};

export default fetch;
