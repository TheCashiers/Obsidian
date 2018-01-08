import * as React from "react";
import { ScopeForm } from "../components/Form";
import * as axios from "../configs/AxiosInstance";
import * as api from "../configs/GlobalSettings";
import { FormContainer, IFormProps } from "./FormContainer";

export class ScopeEditContainer extends FormContainer {
    constructor(props: IFormProps) {
        super(props);
        this.state = {
            claims: "",
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
            const claimString = JSON.stringify(response.data.claims);
            this.setState(response.data);
            this.setState({claims: claimString});
        } catch (error) {
            this.props.push("getScope", error.toString());
        }
    }
    public async handleSubmit(e: React.MouseEvent<HTMLFormElement>) {
        e.preventDefault();
        const scopeName: string = this.state.scopeName.trim();
        const displayName: string = this.state.displayName.trim();
        const description: string = this.state.description.trim();
        const claims: string[] = JSON.parse(this.state.claims);
        if (scopeName && displayName && description && claims) {
            const jsonObject = {
                displayName,
                scopeName,
                description,
                claims,
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
                claimTypes={this.state.claims}
                action="Edit Scope"
            />);
    }
}
