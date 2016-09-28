import * as React from "react";

import * as axios from "axios";
import * as api from "../configs/GlobalSettings";
import { CreateUser } from "../components/CreateUser";
import { UserFormContainer } from "../containers/UserFormContainer"


export class UserCreationContainer extends UserFormContainer
{
    constructor(props) {
        super(props);
        this.state={ username:props.location.query.username, password: "", isComplete: false ,isError:false};
    }
    handleSubmit(e) {
        e.preventDefault();
        console.log(this.props);
        let username: string = this.state.username.trim();
        let password: string = this.state.password.trim();
        if (username && password) {
            axios.post(api.configs.createUser.request_uri, { userName: username, password: password })
                .then(()=>{
                    this.setState({ isComplete: true });
                    this.setState({ username: "", password: "" });
                })
                .catch((e) => this.setState({ isError: true }));
        } else { return; }
    }
    public render() {
        return (<CreateUser
            onInputChange={this.handleInputChange}
            onSubmit={this.handleSubmit}
            username={this.state.username}
            password={this.state.password}
            isComplete={this.state.isComplete}
            isError={this.state.isError}
            action={this.props.action}/>);
    }
}

