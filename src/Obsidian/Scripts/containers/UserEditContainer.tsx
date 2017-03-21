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
    public async componentWillMount() {
        if (this.state.id) {
            try {
                const response = await axios.getAxios(this.props.token).get(api.configs.getUser.request_uri + this.state.id);
                this.setState({ username: response.data.userName });
            } catch (error) {
                Notification.Service.pushError("getClient", error);
            }
        }
        else {
            history.pushState(null,null,"/manage")
            history.go();
        }
    }
    async handleSubmit(e) {
        e.preventDefault();
        let username: string = this.state.username.trim();
        let password: string = this.state.password.trim();
        if (username && password) {
            if (username != this.defaultUsername) {
                try {
                    const payload = { username: username };
                    await axios.getAxios(this.props.token).put(`${api.configs.editUser.request_uri}${this.props.location.query.id}/UserName`, payload);
                    Notification.Service.pushSuccess("Username changing");
                } catch (error) {
                    Notification.Service.pushError("Username changing", e)
                }
            }
            if (password != "") {
                try {
                    const payload = { password: password };
                    await axios.getAxios(this.props.token).put(`${api.configs.editUser.request_uri}${this.props.location.query.id}/PassWord`, payload);
                    Notification.Service.pushSuccess("Password changing");
                } catch (error) {
                    Notification.Service.pushError("Password changing", error)
                }
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