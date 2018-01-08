import * as React from "react";
import { UserForm } from "../components/Form";
import * as axios from "../configs/AxiosInstance";
import * as api from "../configs/GlobalSettings";
import { FormContainer, IFormProps } from "../containers/FormContainer";

export class UserCreationContainer extends FormContainer {
    constructor(props: IFormProps) {
        super(props);
        this.state = { username: "", password: "", id: "" };
        this.handleSubmit = this.handleSubmit.bind(this);
    }
    public async handleSubmit(e: React.MouseEvent<HTMLFormElement>) {
        e.preventDefault();
        const username: string = this.state.username.trim();
        const password: string = this.state.password.trim();
        if (username && password) {
            const payload = { userName: username, password };
            try {
                await axios.getAxios(this.props.token).post(api.configs.createUser.request_uri, payload);
                this.setState({ username: "", password: "" });
                this.props.push("User creation");
            } catch (error) {
                this.props.push("User creation", error.toString());
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
                action="Create User"
            />);
    }
}
