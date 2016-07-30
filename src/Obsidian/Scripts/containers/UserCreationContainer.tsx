import * as React from "react";

import { UserList } from "../components/UserManagement";
import * as axios from "axios";
import * as api from "../configs/GlobalSettings";
import { Main } from "../components/Main";
import { CreateUser } from "../components/CreateUser";

interface UserCreationState {
    username?: string;
    password?: string;
}

export class UserCreationContainer extends React.Component<any, UserCreationState>{
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
        let username = this.state.username.trim();
        let password = this.state.password.trim();
        if (username && password) {
            //TODO:send login requeset
            console.log("creating: " + username +" /w "+ password)
            this.setState({ username: "", password: "" });
        }
        else return;
    }
    public render() {
        return (<CreateUser onUsernameChange={this.handleUsernameChange} onPasswordChange={this.handlePasswordChange} onSubmit={this.handleSubmit}
            username={this.state.username} password={this.state.password}/>)
    }

} 
