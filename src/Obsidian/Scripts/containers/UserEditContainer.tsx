import * as React from "react"
import { UserFormContainer } from "./UserFormContainer"
import { EditUser } from "../components/EditUser";
import * as api from "../configs/GlobalSettings";
import * as axios from "axios";
import * as Notification from "./NotificationContainer"


export class UserEditContainer extends UserFormContainer
{
    constructor(props) {
        super(props);
        this.state={ username:props.location.query.username, password: "defaultState"};
    }
    handleSubmit(e) {
        e.preventDefault();
        let username: string = this.state.username.trim();
        let password: string = this.state.password.trim();
        if (username && password) {
            axios.put(`${api.configs.editUser.request_uri}${this.props.location.query.id}/UserName`, {username:username})
                .then(()=>{
                    Notification.Service.push("Username changed.",Notification.NotificationState.success);
                })
                .catch((e) => Notification.Service.push(`Username changing failed. ${e.toString()}.`,Notification.NotificationState.error));
            if( password!="defaultState" ){
                axios.put(`${api.configs.editUser.request_uri}${this.props.location.query.id}/PassWord`, {password:password})
                .then(()=>{
                    Notification.Service.push("Password changed.",Notification.NotificationState.success);
                })
                .catch((e) => Notification.Service.push(`Password changing failed. ${e.toString()}.`,Notification.NotificationState.error));
            }
        } else { return; }
        
    }
    public render() {
        return (<EditUser
            onInputChange={this.handleInputChange}
            onSubmit={this.handleSubmit}
            username={this.state.username}
            password={this.state.password}
            action={this.props.action}/>);
    }
}