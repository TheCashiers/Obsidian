import * as React from "react";
import * as ReactDOM from "react-dom";
import * as api from "./configs/GlobalSettings";
import { routes } from "./configs/OAuthRoutes";

ReactDOM.render(
    routes,
    document.getElementById("oauth")
);