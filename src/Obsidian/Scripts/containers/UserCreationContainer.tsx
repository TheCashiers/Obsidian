import * as React from "react";

import * as axios from "axios";
import * as api from "../configs/GlobalSettings";
import { CreateUser } from "../components/CreateUser";

interface IUserCreationState {
    username?: string;
    password?: string;
}

export class UserCreationContainer extends React.Component<any, IUserCreationState> {
    constructor(props: any) {
        super(props);
        this.state = { username: "", password: "" };
        this.handlePasswordChange = this.handlePasswordChange.bind(this);
        this.handleUsernameChange = this.handleUsernameChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }
    handleUsernameChange(e) {
        this.setState({
            username: e.target.value as string
        });
    }
    handlePasswordChange(e) {
        this.setState({
            password: e.target.value as string
        });
    }
    handleSubmit(e) {
        e.preventDefault();
        let username: string = this.state.username.trim();
        let password: string = this.state.password.trim();
        if (username && password) {
            // todo:send login requeset
            console.log(`creating: ${username} /w ${password}`);
            this.setState({ username: "", password: "" });
        } else { return; }
    }
    public render() {
        return (<CreateUser
            onUsernameChange={this.handleUsernameChange}
            onPasswordChange={this.handlePasswordChange}
            onSubmit={this.handleSubmit}
            username={this.state.username}
            password={this.state.password}/>);
    }
};
