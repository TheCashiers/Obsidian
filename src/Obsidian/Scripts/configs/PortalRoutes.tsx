// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
import * as React from "react";

import { Router, Route, IndexRoute, browserHistory } from "react-router";
import { Main } from "../components/Main";
import { UserManagementContainer } from "../containers/UserManagementContainer";
import { Portal } from "../components/Portal";
import { UserCreationContainer } from "../containers/UserCreationContainer";
import { ClientCreationContainer } from "../containers/ClientCreationContainer"
import { UserEditContainer } from "../containers/UserEditContainer";
import { ClientManagementContainer } from "../containers/ClientManagementContainer";

export const routes = (
    <Router history={ browserHistory }>
        <Route path="/manage" component={ Main }>
            <IndexRoute component={ Portal }/>
            <Route path="/manage/users" component={ UserManagementContainer } />
            <Route path="/manage/users/create" component={ UserCreationContainer } />
            <Route path="/manage/users/edit" component={ UserEditContainer } />
            <Route path="/manage/clients" component={ ClientManagementContainer } />
            <Route path="/manage/clients/create" component={ ClientCreationContainer } />
        </Route>
    </Router>
);

