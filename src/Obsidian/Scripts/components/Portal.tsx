// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
import * as React from "react";
import { Router, Route, Link, IndexRoute, hashHistory } from "react-router";

export const Portal = (props) => {
    return (
        <Link to="/users">
            <button className="btn btn-lg btn-success">Go to User Management</button>
        </Link>
    )
}