import * as React from "react";
import { ClientForm } from "../components/Form";
import { FormContainer } from "./FormContainer";
import * as axios from "../configs/AxiosInstance";
import * as api from "../configs/GlobalSettings";
import * as Notification from "./NotificationContainer"


export class ClientCreationContainer extends FormContainer
{
    constructor(props: any) {
        super(props);
        this.state = { displayName:"", redirectUri:""};
        this.handleInputChange = this.handleInputChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }
    handleSubmit(e) {
        e.preventDefault();
        let displayName: string = this.state.displayName.trim();
        let redirectUri: string = this.state.redirectUri.trim();
        if (displayName && redirectUri) {
            axios.getAxios(this.props.token).post(api.configs.createClient.request_uri,
                { displayName: displayName, redirectUri: redirectUri },
                
            )
                .then(()=>{
                    this.setState({ displayName: "", redirectUri: "" });
                    console.log(e);
                    Notification.Service.pushSuccess("Client creation")
                })
                .catch((e) =>  Notification.Service.pushError("Client creation",e));
        } else { return; }
    }
    render(){
        return <ClientForm
            action="Create Client"
            onInputChange={this.handleInputChange}
            onSubmit={this.handleSubmit}
            displayName={this.state.displayName}
            redirectUri={this.state.redirectUri}
        />
    }
}