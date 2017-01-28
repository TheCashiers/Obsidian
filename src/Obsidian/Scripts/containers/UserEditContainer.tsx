import * as React from "react"
import { UserFormContainer } from "./UserFormContainer"
import { EditUser } from "../components/EditUser";
import * as api from "../configs/GlobalSettings";
import * as axios from "axios";
import * as Notification from "./NotificationContainer"


export class UserEditContainer extends UserFormContainer {
    public defaultUsername: string;
    constructor(props) {
        super(props);
        this.state = { username: props.location.query.username, password: "" };
        this.defaultUsername = props.location.query.username;
    }
    handleSubmit(e) {
        e.preventDefault();
        let username: string = this.state.username.trim();
        let password: string = this.state.password.trim();
        if (username && password) {
            if (username != this.defaultUsername)
                axios.put(`${api.configs.editUser.request_uri}${this.props.location.query.id}/UserName`, { username: username })
                    .then(() => {
                        Notification.Service.pushSuccess("Username changing");
                    })
                    .catch((e) => Notification.Service.pushError("Username changing", e));
            if (password != "") {
                axios.put(`${api.configs.editUser.request_uri}${this.props.location.query.id}/PassWord`, { password: password })
                    .then(() => {
                        Notification.Service.pushSuccess("Password changing");
                    })
                    .catch((e) => Notification.Service.pushError("Password changing", e));
            }
        } else { return; }

    }
    public render() {
        return (<EditUser
            onInputChange={this.handleInputChange}
            onSubmit={this.handleSubmit}
            username={this.state.username}
            password={this.state.password}
            action={this.props.action} />);
    }
}