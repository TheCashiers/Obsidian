import * as React from "react";
import * as ReactDOM from "react-dom";
import * as api from "./configs/GlobalSettings";
import { routes } from "./configs/PortalRoutes";

ReactDOM.render(
    routes,
    document.getElementById("portal")
);