// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
import * as React from "react";

export const CreateUser = (props) => (
    <div>
        <form onSubmit={props.onSubmit}>
            Username: <input type="text" onChange={props.onUsernameChange} value={props.username}></input>
            Password: <input type="password" onChange={props.onPasswordChange} value={props.password}></input>
            <button className="btn btn-lg btn-success" type="submit">Create!</button>
        </form>
    </div>
);