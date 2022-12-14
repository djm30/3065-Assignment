import { toast } from "react-toastify";
import { Kinds, ResponseTypes } from "../types";
import { ServiceURLS } from "../services/service_urls";
import axios, { Axios, AxiosError } from "axios";

const ServiceUrls = ServiceURLS.getInstance();

// Single hook used for all data fetching
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
            setResult(result);
        } catch (e) {
            console.log(e);
            if (axios.isAxiosError(e)) {
                if (e.response?.status === 400) {
                    toast.update(id, {
                        type: "error",
                        render: e.response?.data.errorMessage,
                        isLoading: false,
                        autoClose: 4000,
                    });
                } else if (e.response?.status && e.response.status >= 500) {
                    // Changing what proxy the server is using
                    ServiceUrls.ChangeProxy();
                    toast.update(id, {
                        type: "error",
                        render: "Switching server, please try again!",
                        isLoading: false,
                        autoClose: 4000,
                    });
                }
            } else {
                // Changing what proxy the server is using
                ServiceUrls.ChangeProxy();
                toast.update(id, {
                    type: "error",
                    render: "Switching server, please try again!",
                    isLoading: false,
                    autoClose: 4000,
                });
            }
        }
    };
};

export { useFetch };
