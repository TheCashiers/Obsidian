import * as React from "react";

import * as axios from "axios";
import * as api from "../configs/GlobalSettings";
import { CreateUser } from "../components/CreateUser";

interface IUserCreationState {
    username?: string;
    password?: string;
    showComplete?: boolean;
}

export class UserCreationContainer extends React.Component<any, IUserCreationState> {
    constructor(props: any) {
        super(props);
        this.state = { username: "", password: "", showComplete: false };
        this.handleInputChange = this.handleInputChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }
    handleInputChange(e) {
        this.setState({
            [e.target.name]: e.target.value as string
        });
    }
    handleSubmit(e) {
        e.preventDefault();
        let username: string = this.state.username.trim();
        let password: string = this.state.password.trim();
        if (username && password) {
            axios.post(api.configs.createUser.request_uri, { userName: username, password: password })
                .then(()=>this.setState({ showComplete: true }))
                .catch((e) => console.warn(e));
            this.setState({ username: "", password: "" });
        } else { return; }
    }
    public render() {
        return (<CreateUser
            onInputChange={this.handleInputChange}
            onSubmit={this.handleSubmit}
            username={this.state.username}
            password={this.state.password}
            completed={this.state.showComplete}/>);
    }
};