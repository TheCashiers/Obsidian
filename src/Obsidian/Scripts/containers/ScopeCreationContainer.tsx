import * as React from "react";
import { ScopeForm } from "../components/Form";
import * as axios from "../configs/AxiosInstance";
import * as api from "../configs/GlobalSettings";
import { FormContainer } from "./FormContainer";

export class ScopeCreationContainer extends FormContainer {
    constructor(props: any) {
        super(props);
        this.state = {
            claimTypes: "",
            description: "",
            displayName: "",
            scopeName: "",
        };
        this.handleSubmit = this.handleSubmit.bind(this);
    }
    public async handleSubmit(e: React.MouseEvent<HTMLFormElement>) {
        e.preventDefault();
        const scopeName: string = this.state.scopeName.trim();
        const displayName: string = this.state.displayName.trim();
        const description: string = this.state.description.trim();
        const claimTypes: string[] = (this.state.claimTypes as string).split(",");
        if (scopeName && displayName && description && claimTypes) {
            const jsonObject = {
                displayName,
                scopeName,
                description,
                claimTypes,
            };
            try {
                await axios.getAxios(this.props.token).post(api.configs.createScope.request_uri, jsonObject);
                this.props.push("Scope creation");
            } catch (error) {
                this.props.push("Scope creation", error.toString());
            }
        } else { return; }
    }
    public render() {
        return (
            <ScopeForm
                action="Create Scope"
                onInputChange={this.handleInputChange}
                onSubmit={this.handleSubmit}
                scopeName={this.state.scopeName}
                displayName={this.state.displayName}
                description={this.state.description}
                claimTypes={this.state.claimTypes}
            />
        );
    }
}
