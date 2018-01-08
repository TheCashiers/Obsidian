import * as React from "react";
import { ClientForm } from "../components/Form";
import * as axios from "../configs/AxiosInstance";
import * as api from "../configs/GlobalSettings";
import { FormContainer, IFormProps } from "./FormContainer";

export class ClientCreationContainer extends FormContainer {
    constructor(props: IFormProps) {
        super(props);
        this.state = { displayName: "", redirectUri: "" };
        this.handleInputChange = this.handleInputChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }
    public async handleSubmit(e: Event) {
        e.preventDefault();
        const displayName: string = this.state.displayName.trim();
        const redirectUri: string = this.state.redirectUri.trim();
        if (displayName && redirectUri) {
            try {
                const payload = { displayName, redirectUri };
                await axios.getAxios(this.props.token).post(api.configs.createClient.request_uri, payload);
                this.setState({ displayName: "", redirectUri: "" });
                this.props.push("Client creation");
            } catch (error) {
                this.props.push("Client creation", error.toString());
            }
        } else { return; }
    }
    public render() {
        return (
            <ClientForm
                action="Create Client"
                onInputChange={this.handleInputChange}
                onSubmit={this.handleSubmit}
                displayName={this.state.displayName}
                redirectUri={this.state.redirectUri}
            />
        );
    }
}
