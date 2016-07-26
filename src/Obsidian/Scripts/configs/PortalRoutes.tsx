// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
import * as React from "react";

import { Router, Route, Link, IndexRoute, hashHistory } from "react-router";
import { Main } from "../components/Main";
import { UserManagementContainer } from "../containers/UserManagementContainer";
import { Portal } from "../components/Portal";

export var routes = (
    <Router history={ hashHistory }>
        <Route path="/" component={ Main }>
            <IndexRoute component={ Portal }/>
            <Route path="/users" component={ UserManagementContainer } />
        </Route>
    </Router>
);

