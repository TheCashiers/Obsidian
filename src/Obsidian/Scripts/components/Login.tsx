// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX

import * as React from "react";
import * as ReactDOM from "react-dom";
import * as api from "../configs/GlobalSettings"



interface ILoginStates {
    username?: string;
    password?: string;
}

export class LoginComponent extends React.Component<api.IDefaultProps, ILoginStates>{
    public state: ILoginStates;
    constructor(props: api.IDefaultProps) {
        super(props);
        this.state = {
            username: "", password: ""
        }
    }
    public handleUsernameChange(e){
        this.setState({
            username: e.target.value as string
        });
    }
    public handlePasswordChange(e) {
        this.setState({
            password: e.target.value as string
        });
    }
    public handleSubmit(e) {
        e.preventDefault();
        let username = this.state.username.trim();
        let password = this.state.password.trim();
        if (username && password) {
            //TODO:send login requeset
            this.setState({ username: "", password: "" });
        }
        else return;
    }
    public render() {
        return (
            <form onSubmit={this.handleSubmit}>
                <input
                    type="text"
                    placeholder="Username"
                    value={this.state.username}
                    onChange={this.handleUsernameChange}
                    />
                <input
                    type="password"
                    placeholder="Password"
                    value={this.state.password}
                    onChange={this.handlePasswordChange}
                    />
                <button type="submit">LOGIN</button>
            </form>
        );
    }
}
