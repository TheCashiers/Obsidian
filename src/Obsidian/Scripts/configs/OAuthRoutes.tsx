import * as React from "react";

import { Router, Route, IndexRoute, createMemoryHistory  } from "react-router";
import { SignInContainer } from "../containers/SignInContainer";
import { UserManagementContainer } from "../containers/UserManagementContainer";
import { UserCreationContainer } from "../containers/UserCreationContainer"

export const routes = (
    <Router history={ createMemoryHistory() }>
        <Route path="/">
            <IndexRoute component={ SignInContainer }/>
        </Route>
    </Router>
    
);

