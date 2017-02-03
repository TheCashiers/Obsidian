import * as React from "react"
import { UserFormContainer } from "./UserFormContainer"
import { ScopeForm } from "../components/Form";
import * as api from "../configs/GlobalSettings";
import * as axios from "axios";
import * as Notification from "./NotificationContainer"


export class ScopeEditContainer extends React.Component<any, any> {
    constructor(props) {
        super(props);
        this.state = {
            id: props.location.query.id,
            scopeName: "",
            displayName: "",
            description: "",
            claimTypes: []
        };
        this.handleInputChange = this.handleInputChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }
    public componentDidMount() {
        axios.get(api.configs.getScope.request_uri + this.state.id)
            .then((info) => { this.setState(info.data); })
            .catch((e) => Notification.Service.pushError("getClient", e));
    }
    handleInputChange(e) {
        this.setState({
            [e.target.name]: e.target.value as string
        });
    }
    handleSubmit(e) {
        e.preventDefault();
        let scopeName: string = this.state.scopeName.trim();
        let displayName: string = this.state.displayName.trim();
        let description: string = this.state.description.trim();
        let claimTypes: string[] = this.state.claimTypes
        if (scopeName && displayName && description && claimTypes) {
            let jsonObject = {
                displayName: displayName,
                scopeName: scopeName,
                description: description,
                claimTypes: claimTypes
            }
            axios
            axios.put(api.configs.getScope.request_uri + this.state.id, jsonObject)
                .then(() => {
                    Notification.Service.pushSuccess("Scope editing")
                })
                .catch((e) => Notification.Service.pushError("Scope editing", e));
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