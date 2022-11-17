import fetch from "./fetch";
import axios from "axios";
import { ServiceURLS } from "./service_urls";
import { ResponseTypes } from "../types";

const services = ServiceURLS.getInstance();

class SessionManagement {
    private static instance: SessionManagement;
}

interface CreateSessionResponse {
    sessionId: string;
}

const createSession = async () => {
    const url =
        services.GetProxy() + services.routes.session + "/createsession";
    const { data } = await axios.post<CreateSessionResponse>(url);
    return data.sessionId;
};

const getOrCreateSession = async () => {
    let id = localStorage.getItem("session");
    if (!id) {
        id = await createSession();
        localStorage.setItem("session", id);
    }
    return id;
};

const retrieveSession = async (sessionId: string) => {
    const url =
        services.GetProxy() +
        services.routes.session +
        "/retrievesession?sessionId=" +
        sessionId;
    const { data } = await axios.get(url);
    return data;
};
const updateSession = async (sessionId: string, ressult: ResponseTypes) => {
    const url =
        services.GetProxy() +
        services.routes.session +
        "/updatesession?sessionId=" +
        sessionId;
    await axios.put(url, ressult);
};
const deleteSession = async (sessionId: string) => {
    localStorage.clear();
    const url =
        services.GetProxy() +
        services.routes.session +
        "/deletesession?sessionId=" +
        sessionId;
    await axios.delete(url);
};

export default {
    createSession,
    getOrCreateSession,
    retrieveSession,
    updateSession,
    deleteSession,
};
