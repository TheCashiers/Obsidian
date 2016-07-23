// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
import * as React from "react";
import * as ReactDOM from "react-dom";

import { Router, Route, Link } from "react-router";
import { Main } from "../components/Global";
import { UserControlContainer } from "../containers/UserManagementContainer";

export var routes = (
    <Router>
        <Route path="/manage" component={ Main }>
            <Route path="/users" component={ UserControlContainer }/>
        </Route>
    </Router>
);