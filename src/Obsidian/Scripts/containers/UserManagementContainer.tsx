import * as React from "react";

import { UserList } from "../components/UserManagement";
import * as axios from "axios";
import * as api from "../configs/GlobalSettings";
import { Main } from "../components/Main";





export class UserManagementContainer extends React.Component<any, any> {
    constructor(props: any) {
        super(props);
        this.state = {
            users: [],
            isLoading: true
        };
    }
    public componentDidMount() {
        axios.get(api.configs.getUser.request_uri)
            .then((info) => { this.setState({users:info.data}) });
    }
    public render() {
        return (
            <UserList users={this.state.users}/>
        );
    }

}