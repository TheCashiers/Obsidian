import * as React from "react";
import { UserList } from "../components/UserManagement";
import * as axios from "../configs/AxiosInstance";
import * as api from "../configs/GlobalSettings";

interface IUserManagementState {
    users: any[];
}

export class UserManagementContainer extends React.Component<any, IUserManagementState> {
    constructor(props: any) {
        super(props);
        this.state = {
            users: [],
        };
        this.getUsers = this.getUsers.bind(this);
    }
    public async componentDidMount() {
        try {
            const response = await axios.getAxios(this.props.token).get(api.configs.getUser.request_uri);
            this.setState({ users: response.data });
        } catch (error) {
            this.props.push("getUser", error.toString());
        }
    }
    public getUsers() {
        return (this.state.users as any[]).filter((_, i) => (_.userName as string).includes(this.props.filter));
    }

    public render() {
        return (
            <UserList
                users={this.getUsers()}
            />
        );
    }

}
