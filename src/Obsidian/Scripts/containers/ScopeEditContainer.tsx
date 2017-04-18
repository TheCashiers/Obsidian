import * as React from "react";
import { ScopeForm } from "../components/Form";
import * as axios from "../configs/AxiosInstance";
import * as api from "../configs/GlobalSettings";
import { FormContainer } from "./FormContainer";

export class ScopeEditContainer extends FormContainer {
    constructor(props) {
        super(props);
        this.state = {
            claimTypes: [],
            description: "",
            displayName: "",
            id: props.location.query.id,
            scopeName: "",
        };
        this.handleSubmit = this.handleSubmit.bind(this);
    }
    public async componentWillMount() {
        try {
            const response = await axios.getAxios(this.props.token)
                .get(api.configs.getScope.request_uri + this.state.id);
            this.setState(response.data);

        } catch (error) {
            this.props.push("getClient", error.toString());
        }
    }
    public async handleSubmit(e) {
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
                await axios.getAxios(this.props.token)
                    .put(api.configs.getScope.request_uri + this.state.id, jsonObject);
                this.props.push("Scope editing");
            } catch (error) {
                this.props.push("Scope editing", error.toString());
            }
        } else { return; }
    }
    public render() {
        return (
            <ScopeForm
                onInputChange={this.handleInputChange}
                onSubmit={this.handleSubmit}
                scopeName={this.state.scopeName}
                displayName={this.state.displayName}
                description={this.state.description}
                claimTypes={this.state.claimTypes}
                action="Edit Scope"
            />);
    }
}
