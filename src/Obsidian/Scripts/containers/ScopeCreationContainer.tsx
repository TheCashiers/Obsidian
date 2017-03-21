import * as React from "react";
import * as axios from "../configs/AxiosInstance";
import * as api from "../configs/GlobalSettings";
import { FormContainer } from "./FormContainer";
import * as Notification from "./NotificationContainer";
import { ScopeForm } from "../components/Form"

export class ScopeCreationContainer extends FormContainer {
    constructor(props: any) {
        super(props);
        this.state = {
            claimTypes: "",
            scopeName: "",
            displayName: "",
            description: ""
        };
        this.handleSubmit = this.handleSubmit.bind(this);
    }
    async handleSubmit(e) {
        e.preventDefault();
        let scopeName: string = this.state.scopeName.trim();
        let displayName: string = this.state.displayName.trim();
        let description: string = this.state.description.trim();
        let claimTypes: string[] = (this.state.claimTypes as string).split(",");
        if (scopeName && displayName && description && claimTypes) {
            let jsonObject = {
                displayName: displayName,
                scopeName: scopeName,
                description: description,
                claimTypes: claimTypes
            };
            try {
                await axios.getAxios(this.props.token).post(api.configs.createScope.request_uri, jsonObject);
                console.log(e);
                Notification.Service.pushSuccess("Scope creation")
            } catch (error) {
                Notification.Service.pushError("Scope creation", error);
            }
        } else { return; }
    }
    render() {
        return <ScopeForm
            action="Create Scope"
            onInputChange={this.handleInputChange}
            onSubmit={this.handleSubmit}
            scopeName={this.state.scopeName}
            displayName={this.state.displayName}
            description={this.state.description}
            claimTypes={this.state.claimTypes}
        />
    }
}