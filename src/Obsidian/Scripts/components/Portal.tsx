// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
import * as React from "react";
import { Link } from "react-router";

export const Portal = (props) => {
    return (
        <div className="content-wrapper">
            <Link to="/manage/users">
                <button className="btn btn-lg btn-success">Go to User Management</button>
            </Link>
        </div>
    )
}