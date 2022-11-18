import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App";
import "./index.css";

fetch(
    "http://proxy2.40265748.qpc.hal.davecutting.uk/classify?&module_1=CS%2021&module_2=&module_3=&module_4=&module_5=&mark_1=100&mark_2=&mark_3=&mark_4=&mark_5=",
).then((res) =>
    res.body
        ?.getReader()
        .read()
        .then((res) => console.log(res)),
);

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
    <App />,
);
