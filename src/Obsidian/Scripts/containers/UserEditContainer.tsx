import * as React from "react";
import { UserForm } from "../components/Form";
import * as axios from "../configs/AxiosInstance";
import * as api from "../configs/GlobalSettings";
import { FormContainer } from "./FormContainer";

export class UserEditContainer extends FormContainer {
    public defaultUsername: string;
    constructor(props) {
        super(props);
        this.state = { username: "", password: "", id: props.location.query.id };
        this.defaultUsername = props.location.query.username;
    }
    public async componentWillMount() {
        if (this.state.id) {
            try {
                const response = await axios.getAxios(this.props.token)
                    .get(api.configs.getUser.request_uri + this.state.id);
                this.setState({ username: response.data.userName });
            } catch (error) {
                this.props.push("getUser", error.toString());
            }
        } else {
            history.pushState(null, null, "/manage");
            history.go();
        }
    }
    public async handleSubmit(e) {
        e.preventDefault();
        const username: string = this.state.username.trim();
        const password: string = this.state.password.trim();
        if (username && password) {
            if (username !== this.defaultUsername) {
                try {
                    const payload = { username };
                    await axios.getAxios(this.props.token)
                        .put(`${api.configs.editUser.request_uri}${this.props.location.query.id}/UserName`, payload);
                    this.props.push("Username changing");
                } catch (error) {
                    this.props.push("Username changing", error.toString());
                }
            }
            if (password !== "") {
                try {
                    const payload = { password };
                    await axios.getAxios(this.props.token)
                        .put(`${api.configs.editUser.request_uri}${this.props.location.query.id}/PassWord`, payload);
                    this.props.push("Password changing");
                } catch (error) {
                    this.props.push("Password changing", error.toString());
                }
            }
        } else { return; }
    }
    public render() {
        return (
            <UserForm
                onInputChange={this.handleInputChange}
                onSubmit={this.handleSubmit}
                username={this.state.username}
                password={this.state.password}
                action="Edit User"
            />);
    }
}
