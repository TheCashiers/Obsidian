import * as React from "react"
import { UserFormContainer } from "./UserFormContainer"
import { ScopeForm } from "../components/Form";
import * as api from "../configs/GlobalSettings";
import { FormContainer } from "./FormContainer";
import * as axios from "../configs/AxiosInstance";


export class ScopeEditContainer extends FormContainer {
    constructor(props) {
        super(props);
        this.state = {
            id: props.location.query.id,
            scopeName: "",
            displayName: "",
            description: "",
            claimTypes: []
        };
        this.handleSubmit = this.handleSubmit.bind(this);
    }
    public async componentWillMount() {
        try {
            const response = await axios.getAxios(this.props.token).get(api.configs.getScope.request_uri + this.state.id)
            this.setState(response.data);

        } catch (error) {
            this.props.push("getClient", error);
        }
    }
    handleSubmit(e) {
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
            }
            axios.getAxios(this.props.token).put(api.configs.getScope.request_uri + this.state.id, jsonObject)
                .then(() => {
                    this.props.push("Scope editing")
                })
                .catch((e) => this.props.push("Scope editing", e.toString()));
        } else { return; }
    }
    public render() {
        return (<ScopeForm
            onInputChange={this.handleInputChange}
            onSubmit={this.handleSubmit}
            scopeName={this.state.scopeName}
            displayName={this.state.displayName}
            description={this.state.description}
            claimTypes={this.state.claimTypes}
            action="Edit Scope" />);
    }
}