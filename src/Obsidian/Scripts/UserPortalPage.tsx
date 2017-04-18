// A '.tsx' file enables JSX support in the TypeScript compiler,
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
import * as React from "react";
import * as ReactDOM from "react-dom";
import * as api from "./configs/GlobalSettings";
import { routes } from "./configs/PortalRoutes";

import { UserManagementContainer } from "./containers/UserManagementContainer";

ReactDOM.render(
    routes,
    document.getElementById("portal"),
);
