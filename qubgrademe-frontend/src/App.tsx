import { useState, useEffect } from "react";
import Button from "./components/Button";
import Container from "./components/Container";
import Display from "./components/Display";
import Module from "./components/Module";
import getMaxMin from "./services/maxMin";
import getSorted from "./services/sort";
import getTotal from "./services/totalMarks";
import getClassification from "./services/classifyGrade";
import getMarksNeeded from "./services/marksForNext";
import getMeanMark from "./services/meanMark";
import { toast, ToastContainer } from "react-toastify";
import { ServiceURLS } from "./services/service_urls";
import sessionManagement from "./services/storageService";
import GitHub from "./components/GitHub";

import "react-toastify/dist/ReactToastify.css";

import {
    ClassifyGradeResponse,
    Kinds,
    MarksForNextResponse,
    MeanMarkResponse,
    MinMaxResponse,
    ResponseTypes,
    SortedResponse,
    TotalMarksResponse,
} from "./types";
import { useFetch } from "./hooks";

function App() {
    const [module1, setModule1] = useState("");
    const [module2, setModule2] = useState("");
    const [module3, setModule3] = useState("");
    const [module4, setModule4] = useState("");
    const [module5, setModule5] = useState("");

    const [mark1, setMark1] = useState<number>(NaN);
    const [mark2, setMark2] = useState<number>(NaN);
    const [mark3, setMark3] = useState<number>(NaN);
    const [mark4, setMark4] = useState<number>(NaN);
    const [mark5, setMark5] = useState<number>(NaN);

    const marks = [mark1, mark2, mark3, mark4, mark5];
    const setMarks = [setMark1, setMark2, setMark3, setMark4, setMark5];
    const modules = [module1, module2, module3, module4, module5];
    const setModules = [
        setModule1,
        setModule2,
        setModule3,
        setModule4,
        setModule5,
    ];

    const [result, setResult] = useState<ResponseTypes>();
    const [session, setSession] = useState("");

    useEffect(() => {
        const initialise = async () => {
            await ServiceURLS.getInstance().LoadData();
            const id = await sessionManagement.getOrCreateSession();
            setSession(id);
        };
        initialise().catch(console.error);
    }, []);

    const loadValues = (loadedModules: string[], loadedMarks: string[]) => {
        for (let i = 0; i < modules.length; i++) {
            setModules[i](loadedModules[i]);
            if (loadedModules[i]) setMarks[i](Number(loadedMarks[i]));
        }
    };

    const loadSession = async () => {
        const data = await sessionManagement.retrieveSession(session);
        if (!data) return;
        let result: ResponseTypes | undefined = undefined;
        switch (data.kind) {
            case Kinds.minMax:
                result = data as MinMaxResponse;
                break;
            case Kinds.sort:
                result = data as SortedResponse;
                break;
            case Kinds.total:
                result = data as TotalMarksResponse;
                break;
            case Kinds.classify:
                result = data as ClassifyGradeResponse;
                break;
            case Kinds.marksForNext:
                result = data as MarksForNextResponse;
                break;
            case Kinds.mean:
                result = data as MeanMarkResponse;
                break;
        }
        setResult(result);
        loadValues(result?.modules as string[], result?.marks as string[]);
    };

    useEffect(() => {
        if (session != "") loadSession();
    }, [session]);

    useEffect(() => {
        if (result != undefined)
            sessionManagement.updateSession(session, result as ResponseTypes);
    }, [result]);

    const fetch = useFetch(setResult);

    const onMinMaxClick = async () => {
        await fetch(getMaxMin, modules, marks);
    };

    const onSortClick = async () => {
        await fetch(getSorted, modules, marks);
    };

    const onTotalClick = async () => {
        await fetch(getTotal, modules, marks);
    };

    const onClassifyClick = async () => {
        await fetch(getClassification, modules, marks);
    };

    const onMarksNeededClick = async () => {
        await fetch(getMarksNeeded, modules, marks);
    };

    const onMeanClick = async () => {
        await fetch(getMeanMark, modules, marks);
    };

    const onReloadClick = async () => {
        await loadSession();
    };

    const onChangeClick = async () => {
        let newSession = prompt("Please enter a new session ID");
        if (newSession) {
            if (
                /^[0-9a-f]{8}-[0-9a-f]{4}-[0-5][0-9a-f]{3}-[089ab][0-9a-f]{3}-[0-9a-f]{12}$/i.test(
                    newSession,
                )
            ) {
                localStorage.setItem("session", newSession);
                setSession(newSession);
                toast("Session Updated");
            } else {
                toast.error("Please enter a valid UUID ");
            }
        }
    };

    const onDeleteClick = async () => {
        await sessionManagement.deleteSession(session);
    };

    const onCopyClick = async () => {
        navigator.clipboard.writeText(session);
        toast("Copied to clipboard!");
    };

    const clearInputs = () => {
        setModule1("");
        setModule2("");
        setModule3("");
        setModule4("");
        setModule5("");
        setMark1(NaN);
        setMark2(NaN);
        setMark3(NaN);
        setMark4(NaN);
        setMark5(NaN);
        setResult(undefined);
    };

    return (
        <>
            <div className="bg-neutral-900 h-screen">
                {/* HEADER */}
                <h1 className="text-3xl text-white font-light text-center pt-10">
                    QUB GradeMe App
                </h1>
                {/* TEXT FIELDS */}
                <Container className="mt-10 flex flex-col items-center space-y-2">
                    <Module
                        value={module1}
                        setValue={setModule1}
                        markValue={mark1}
                        setMarkValue={setMark1}
                    />
                    <Module
                        value={module2}
                        setValue={setModule2}
                        markValue={mark2}
                        setMarkValue={setMark2}
                    />
                    <Module
                        value={module3}
                        setValue={setModule3}
                        markValue={mark3}
                        setMarkValue={setMark3}
                    />
                    <Module
                        value={module4}
                        setValue={setModule4}
                        markValue={mark4}
                        setMarkValue={setMark4}
                    />
                    <Module
                        value={module5}
                        setValue={setModule5}
                        markValue={mark5}
                        setMarkValue={setMark5}
                    />
                </Container>
                {/* OUTPUT BOX */}
                <Container className="flex justify-center mt-4">
                    <Display result={result} />
                </Container>
                {/* BUTTONS */}
                <Container className="flex items-center flex-col">
                    {/* BUTTON GRID */}
                    <div className="grid grid-cols-2 w-4/5 mt-4 gap-4">
                        <Button onClick={onMinMaxClick}>
                            Highest & Lowest Scoring Modules
                        </Button>
                        <Button onClick={onSortClick}>Sort Modules</Button>
                        <Button onClick={onTotalClick}>Total Marks</Button>
                        <Button onClick={onClassifyClick}>
                            Classify Grade
                        </Button>
                        <Button onClick={onMarksNeededClick}>
                            Marks Needed for Next Grade
                        </Button>
                        <Button onClick={onMeanClick}>Mean Mark</Button>
                        <Button
                            onClick={clearInputs}
                            className="col-span-2 bg-purple hover:bg-purupleHover"
                        >
                            Clear
                        </Button>
                    </div>

                    {/* CLEAR */}
                </Container>
                <div className="flex justify-between flex-row text-white mt-2">
                    <p> Session ID: {session}</p>
                    <div className="space-x-4 flex flex-row">
                        <button
                            className="bg-blue-400 hover:bg-blue-700 rounded-xl px-4 py-1"
                            onClick={onReloadClick}
                        >
                            Reload
                        </button>
                        <button
                            className="bg-blue-400 hover:bg-blue-700 rounded-xl px-4 py-1"
                            onClick={onChangeClick}
                        >
                            Change
                        </button>
                        <button
                            className="bg-blue-400 hover:bg-blue-700 rounded-xl px-4 py-1"
                            onClick={onDeleteClick}
                        >
                            Delete
                        </button>
                        <button
                            className="bg-blue-400 hover:bg-blue-700 rounded-xl px-4 py-1"
                            onClick={onCopyClick}
                        >
                            Copy
                        </button>
                    </div>
                </div>
                {/* FOOTER */}
            </div>
            <footer className="h-24 mt-20 text-white bg-violet-600 flex justify-center items-center space-x-10">
                <p>Dylan Morrison - 40265748</p>
                <GitHub width="50" height="50" fill="white" />
            </footer>
            <ToastContainer theme="dark" />
        </>
    );
}

export default App;
