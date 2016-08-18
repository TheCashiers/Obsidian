import * as React from "react";

import { Router, Route, IndexRoute, createMemoryHistory  } from "react-router";
import { PortalContentWrapper } from "../components/PortalContentWrapper";
import { UserManagementContainer } from "../containers/UserManagementContainer";
import { Portal } from "../components/Portal";
import { UserCreationContainer } from "../containers/UserCreationContainer"

export const routes = (
    <Router history={ createMemoryHistory() }>
        <Route path="/oauth">
            <IndexRoute component={ Portal }/>
            <Route path="/oauth/signin" component={ UserManagementContainer } />
            <Route path="/manage/users/create" component={ UserCreationContainer } />
        </Route>
    </Router>
    
);

