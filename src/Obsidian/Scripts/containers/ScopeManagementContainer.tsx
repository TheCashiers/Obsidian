import * as React from "react";
import * as axios from "../configs/AxiosInstance";
import * as api from "../configs/GlobalSettings";
import * as Notification from "./NotificationContainer";
import { ScopeList } from "../components/ScopeManagement"

export class ScopeManagementContainer extends React.Component<any, any>
{
    constructor(props: any) {
        super(props);
        this.state = {
            scopes: []
        };
    }
    public componentDidMount() {
        axios.getAxios(this.props.token).get(api.configs.getScope.request_uri)
            .then((info) => { this.setState({ scopes: info.data as Array<any> }); })
            .catch((e) =>  Notification.Service.pushError("getClient",e));
    }
    public render() {
        return (
            <ScopeList scopes={this.state.scopes}/>
        );
    }
}