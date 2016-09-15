// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
import * as React from "react";
import {UserForm} from "./UserForm"

export const CreateUser = (props) => (
    <div className="content-wrapper well">
        <UserForm onSubmit={props.onSubmit} onInputChange={props.onInputChange} username={props.username} password={props.password} action="Create User"/>
        {props.isComplete ?
            <div className="callout callout-success lead">
                <h4>Success</h4>
                <p>
                    User created.
                </p>
            </div>
            : null}
            {
                props.isError ?
                <div className="alert alert-dismissible alert-danger">
                    <button type="button" className="close" data-dismiss="alert">×</button>
                    <strong>Error</strong><br/>
                    An error occured when creating user.
                </div>:null
            }

    </div>
);