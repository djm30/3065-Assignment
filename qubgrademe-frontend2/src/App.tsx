import { useState } from "react";
import Button from "./components/Button";
import Container from "./components/Container";
import Display from "./components/Display";
import Module from "./components/Module";
import getMaxMin from "./services/maxMin";
import getSorted from "./services/sort";
import { MinMaxResponse, SortedResponse } from "./types";

function App() {
  const [module1, setModule1] = useState("");
  const [module2, setModule2] = useState("");
  const [module3, setModule3] = useState("");
  const [module4, setModule4] = useState("");
  const [module5, setModule5] = useState("");

  const [mark1, setMark1] = useState<number>(0);
  const [mark2, setMark2] = useState<number>(0);
  const [mark3, setMark3] = useState<number>(0);
  const [mark4, setMark4] = useState<number>(0);
  const [mark5, setMark5] = useState<number>(0);

  const [result, setResult] = useState<MinMaxResponse | SortedResponse>();

  const onMinMaxClick = async () => {
    const minMax = await getMaxMin(
      module1,
      module2,
      module3,
      module4,
      module5,
      mark1,
      mark2,
      mark3,
      mark4,
      mark5,
    );
    setResult(minMax);
  };
  const onSortClick = async () => {
    const sorted = await getSorted(
      module1,
      module2,
      module3,
      module4,
      module5,
      mark1,
      mark2,
      mark3,
      mark4,
      mark5,
    );
    setResult(sorted);
  };

  return (
    <div className="bg-neutral-800 h-screen">
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
          <Button>Total Marks</Button>
          <Button>Classify Grade</Button>
          <Button>???</Button>
          <Button>???</Button>
          <Button className="col-span-2 bg-red-400 hover:bg-red-500">
            Clear
          </Button>
        </div>

        {/* CLEAR */}
      </Container>
      {/* FOOTER */}
      <footer></footer>
    </div>
  );
}

export default App;
