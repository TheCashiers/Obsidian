// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX

import * as React from "react";
import * as ReactDOM from "react-dom";

//Server-side Config Here
const SERVER_DATA = {
    request_uri: "/api",
    invoke_func: "login"
};


export class LoginComponent extends React.Component<any, any>{
    render() {
        return (
            <div>
                <input type="text" placeholder="Username"/>
                <input type="password" placeholder="Password"/>
                <SubmitButton/>
            </div>
        );
    }
};

class SubmitButton extends React.Component<any, any>{
    render() {
        return <button>Login</button>
    }
}

export const HelloComponent = (props) => <h1>Hello from {this.props.compiler} and {this.props.framework}!</h1>;