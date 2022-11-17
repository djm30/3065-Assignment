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
import { ToastContainer } from "react-toastify";
import { ServiceURLS } from "./services/service_urls";

import "react-toastify/dist/ReactToastify.css";

import { ResponseTypes } from "./types";
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

    const [result, setResult] = useState<ResponseTypes>();

    useEffect(() => {
        // Initiating singleton
        ServiceURLS.getInstance();
    }, []);

    useEffect(() =>
        // Updating session storage information every time the result value is changed
        {}, [result]);

    const fetch = useFetch(setResult);

    const marks = [mark1, mark2, mark3, mark4, mark5];
    const modules = [module1, module2, module3, module4, module5];

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
                {/* FOOTER */}
                <footer className="h-24 mt-20 bg-accentBlue"></footer>
            </div>
            <ToastContainer theme="dark" />
        </>
    );
}

export default App;
