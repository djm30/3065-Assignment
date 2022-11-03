import { useState } from "react";
import Button from "./components/Button";
import Container from "./components/Container";
import Module from "./components/Module";

function App() {
  return (
    <div className="bg-neutral-800 h-screen">
      {/* HEADER */}
      <h1 className="text-3xl text-white font-light text-center pt-10">
        QUB GradeMe App
      </h1>
      {/* TEXT FIELDS */}
      <Container className="mt-10 flex flex-col items-center space-y-2">
        <Module />
        <Module />
        <Module />
        <Module />
        <Module />
      </Container>
      {/* OUTPUT BOX */}
      <Container className="flex justify-center mt-4">
        <textarea
          className="w-4/5 p-4 bg-neutral-900 rounded-lg focus:outline-none border-[0.1px] border-transparent hover:border-neutral-400  text-white transition-all"
          readOnly={true}
          rows={5}
          cols={35}
          placeholder="Results pending"
        ></textarea>
      </Container>
      {/* BUTTONS */}
      <Container className="flex items-center flex-col">
        {/* BUTTON GRID */}
        <div className="grid grid-cols-2 w-4/5 mt-4 gap-4">
          <Button>Highest & Lowest Scoring Modules</Button>
          <Button>Sort Modules</Button>
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
