import * as React from "react";
import { UserList } from "../components/UserManagement";
import * as axios from "axios";
import * as api from "../configs/GlobalSettings";
import * as Notification from "./NotificationContainer"

interface UserManagementState {
    users: Array<any>; 
}


export class UserManagementContainer extends React.Component<any, UserManagementState> {
    constructor(props: any) {
        super(props);
        this.state = {
            users: []
        };
    }
    public componentDidMount() {
        axios.get(api.configs.getUser.request_uri)
            .then((info) => { this.setState({ users: info.data as Array<any> }); })
            .catch((e) =>  Notification.Service.pushError("getUser",e));
    }
    public render() {
        return (
            <UserList users={this.state.users}/>
        );
    }

}