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
        const id = toast.loading("Fetching Results!");
        try {
            const result = await getFunction(modules, marks);
            toast.update(id, {
                type: "default",
                render: "Successful!",
                isLoading: false,
                autoClose: 1500,
            });
            console.log("hello");
            setResult(result);
        } catch (e) {
            if (axios.isAxiosError(e)) {
                if (e.response?.status === 400) {
                    toast.update(id, {
                        type: "error",
                        render: e.response?.data.errorMessage,
                        isLoading: false,
                        autoClose: 4000,
                    });
                }
                if (e.message === "Network Error") {
                    toast.update(id, {
                        type: "error",
                        render: "Can't connect to our servers!",
                        isLoading: false,
                        autoClose: 4000,
                    });
                }
            } else {
                console.log(e);
                toast.update(id, {
                    type: "error",
                    render: "An unknown error occured!",
                    isLoading: false,
                    autoClose: 4000,
                });
            }
        }
    };
};

export { useFetch };
