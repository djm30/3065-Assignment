import { ToastContent } from "react-toastify";
import { toast } from "react-toastify";
import { Kinds, ResponseTypes } from "../types";
import axios from "axios";

const useFetch = (
    setResult: React.Dispatch<React.SetStateAction<ResponseTypes | undefined>>,
) => {
    return async <T extends ResponseTypes>(
        getFunction: (modules: string[], marks: number[]) => Promise<T>,
        modules: string[],
        marks: number[],
    ) => {
        try {
            const result = await getFunction(modules, marks);
            console.log("hello");
            setResult(result);
        } catch (e) {
            if (axios.isAxiosError(e)) {
                if (e.response?.status === 400) {
                    toast.error(e.response?.data.errorMessage);
                }
                if (e.message === "Network Error") {
                    toast.error("Can't connect to our servers");
                }
            } else {
                console.log(e);
                toast.error("An unknown error has occured");
            }
        }
    };
};

export { useFetch };
