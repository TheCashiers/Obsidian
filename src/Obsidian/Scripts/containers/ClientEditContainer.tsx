import * as React from "react";
import { ClientForm } from "../components/Form";
import * as api from "../configs/GlobalSettings";
import * as axios from "axios";
import * as Notification from "./NotificationContainer"


export class ClientEditContainer extends React.Component<any, any> {
    constructor(props) {
        super(props);
        this.state = {
            id: props.location.query.id,
            displayName: "",
            redirectUri: ""
        };
        this.handleInputChange = this.handleInputChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }
    public componentWillMount() {
        axios.get(api.configs.getClient.request_uri + this.state.id)
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
        let displayName: string = this.state.displayName.trim();
        let redirectUri: string = this.state.redirectUri.trim();
        if (displayName && redirectUri) {
            axios.post(api.configs.createClient.request_uri, { displayName: displayName, redirectUri: redirectUri })
                .then(()=>{
                    Notification.Service.pushSuccess("Client editing")
                })
                .catch((e) =>  Notification.Service.pushError("Client creation",e));
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