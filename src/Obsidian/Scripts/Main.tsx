// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
import * as React from "react";
import * as ReactDOM from "react-dom";

import { LoginComponent } from "./components/Login";

ReactDOM.render(
    <LoginComponent />,
    document.getElementById("loginer")
);