import * as React from "react";
import * as Notification from "./NotificationContainer"
import * as axios from "../configs/AxiosInstance";
import * as api from "../configs/GlobalSettings";
import { UserForm } from "../components/Form";
import { UserFormContainer } from "../containers/UserFormContainer"


export class UserCreationContainer extends UserFormContainer
{
    constructor(props) {
        super(props);
        this.state = { username: "", password: "" };
        this.handleSubmit = this.handleSubmit.bind(this);
    }
    handleSubmit(e) {
        e.preventDefault();
        let username: string = this.state.username.trim();
        let password: string = this.state.password.trim();
        if (username && password) {
            axios.getAxios(this.props.token).post(api.configs.createUser.request_uri, { userName: username, password: password })
                .then(()=>{
                    this.setState({ username: "", password: "" });
                    console.log(e);
                    Notification.Service.pushSuccess("User creation")
                })
                .catch((e) =>  Notification.Service.pushError("User creation",e));
        } else { return; }
    }
    public render() {
        return (<UserForm
            onInputChange={this.handleInputChange}
            onSubmit={this.handleSubmit}
            username={this.state.username}
            password={this.state.password}
            action="Create User"/>);
    }
}

