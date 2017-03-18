import * as React from "react"
import { UserFormContainer } from "./UserFormContainer"
import { UserForm } from "../components/Form";
import * as api from "../configs/GlobalSettings";
import * as axios from "../configs/AxiosInstance";
import * as Notification from "./NotificationContainer"


export class UserEditContainer extends UserFormContainer {
    public defaultUsername: string;
    constructor(props) {
        super(props);
        this.state = { username: "", password: "", id: props.location.query.id };
        this.defaultUsername = props.location.query.username;
    }
    public componentWillMount() {
        if (this.state.id)
            axios.getAxios(this.props.token).get(api.configs.getUser.request_uri + this.state.id)
                .then((info) => { this.setState({ username: info.data.userName }); })
                .catch((e) => Notification.Service.pushError("getClient", e));
        else {
            history.pushState(null,null,"/manage")
            history.go();
        }
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
        return (<UserForm
            onInputChange={this.handleInputChange}
            onSubmit={this.handleSubmit}
            username={this.state.username}
            password={this.state.password}
            action="Edit User" />);
    }
}