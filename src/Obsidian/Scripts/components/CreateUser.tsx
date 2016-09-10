// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
import * as React from "react";

export const CreateUser = (props) => (
    <div className="content-wrapper well">
        <form onSubmit={props.onSubmit}>
            Username: <input type="text" name="username" onChange={props.onInputChange} value={props.username}></input>
            Password: <input type="password" name="password" onChange={props.onInputChange} value={props.password}></input>
            <button className="btn btn-lg btn-success" type="submit">Create!</button>
        </form>
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