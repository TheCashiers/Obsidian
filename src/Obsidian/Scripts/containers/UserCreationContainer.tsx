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
        this.state = { username: "", password:"" };
    }
    public handleUsernameChange(e) {
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
        return (<CreateUser onUsernameChange={this.handleUsernameChange} onPasswordChange={this.handlePasswordChange} onSubmit={this.handleSubmit}
            username={this.state.username} password={this.state.password}/>)
    }

}



interface UserManagementState {
    users: Array<any>; 
    isLoading: boolean;
}



export class UserManagementContainer extends React.Component<any, UserManagementState> {
    constructor(props: any) {
        super(props);
        this.state = {
            users: [],
            isLoading: true
        };
    }
    public componentDidMount() {
        axios.get(api.configs.getUser.request_uri)
            .then((info) => { this.setState({ users: info.data as Array<any>, isLoading: false }); })
            .catch((e) => { console.warn("Obsidian Exception: " + e); });
    }
    public render() {
        return (
            <UserList users={this.state.users}/>
        );
    }

}