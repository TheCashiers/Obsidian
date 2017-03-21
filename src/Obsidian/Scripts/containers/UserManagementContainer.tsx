import * as React from "react";
import { UserList } from "../components/UserManagement";
import * as axios from "../configs/AxiosInstance";
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
    public async componentDidMount() {
        try {
            const response = await axios.getAxios(this.props.token).get(api.configs.getUser.request_uri)
            this.setState({ users: response.data as Array<any> });
        } catch (error) {
            Notification.Service.pushError("getUser", error);
        }
    }
    public render() {
        return (
            <UserList users={this.state.users} />
        );
    }

}