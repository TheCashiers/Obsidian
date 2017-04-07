import * as React from "react";
import { ClientForm } from "../components/Form";
import * as api from "../configs/GlobalSettings";
import { FormContainer } from "./FormContainer";
import * as axios from "../configs/AxiosInstance";


export class ClientEditContainer extends FormContainer {
    constructor(props) {
        super(props);
        this.state = {
            id: props.location.query.id,
            displayName: "",
            redirectUri: ""
        };
        this.handleSubmit = this.handleSubmit.bind(this);
    }
    public async componentWillMount() {
        try {
            const response = await axios.getAxios(this.props.token)
                .get(api.configs.getClient.request_uri + this.state.id);
            this.setState(response.data);
        } catch (error) {
            this.props.push("getClient", error.toString())
        }
        
    }
    async handleSubmit(e) {
        e.preventDefault();
        let displayName: string = this.state.displayName.trim();
        let redirectUri: string = this.state.redirectUri.trim();
        if (displayName && redirectUri) {
            try {
                let payload = { displayName: displayName, redirectUri: redirectUri };
                await axios.getAxios(this.props.token)
                    .put(api.configs.createClient.request_uri + this.state.id, payload);
                this.props.push("Client editing");
            } catch (error) {
                this.props.push("Client editing", error.toString());
            }
        } else { return; }
    }
    public render() {
        return (<ClientForm
            onInputChange={this.handleInputChange}
            onSubmit={this.handleSubmit}
            redirectUri={this.state.redirectUri}
            displayName={this.state.displayName}
            action="Edit Client" />);
    }
}