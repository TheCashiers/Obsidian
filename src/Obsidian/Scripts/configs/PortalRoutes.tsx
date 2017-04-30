import * as React from "react";
import { browserHistory, IndexRoute, Route, Router } from "react-router";
import { PortalIndex } from "../components/PortalIndex";
import { ClientCreationContainer } from "../containers/ClientCreationContainer";
import { ClientEditContainer } from "../containers/ClientEditContainer";
import { ClientManagementContainer } from "../containers/ClientManagementContainer";
import { PortalContainer } from "../containers/PortalContainer";
import { ScopeCreationContainer } from "../containers/ScopeCreationContainer";
import { ScopeEditContainer } from "../containers/ScopeEditContainer";
import { ScopeManagementContainer } from "../containers/ScopeManagementContainer";
import { UserCreationContainer } from "../containers/UserCreationContainer";
import { UserEditContainer } from "../containers/UserEditContainer";
import { UserManagementContainer } from "../containers/UserManagementContainer";
export const routes = (
    <Router history={browserHistory}>
        <Route path="/manage" component={PortalContainer}>
            <IndexRoute component={PortalIndex} />
            <Route path="/manage/users" component={UserManagementContainer} />
            <Route path="/manage/users/create" component={UserCreationContainer} />
            <Route path="/manage/users/edit" component={UserEditContainer} />
            <Route path="/manage/clients" component={ClientManagementContainer} />
            <Route path="/manage/clients/create" component={ClientCreationContainer} />
            <Route path="/manage/clients/edit" component={ClientEditContainer} />
            <Route path="/manage/scopes" component={ScopeManagementContainer} />
            <Route path="/manage/scopes/create" component={ScopeCreationContainer} />
            <Route path="/manage/scopes/edit" component={ScopeEditContainer} />
        </Route>
    </Router>
);
