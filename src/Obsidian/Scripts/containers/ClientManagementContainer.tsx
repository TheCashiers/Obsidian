import * as React from "react"
import * as axios from "../configs/AxiosInstance";
import * as api from "../configs/GlobalSettings";
import * as Notification from "./NotificationContainer";
import { ClientList } from "../components/ClientManagement"

export class ClientManagementContainer extends React.Component<any, any> {
    constructor(props: any) {
        super(props);
        this.state = {
            clients: []
        };
    }
    public componentDidMount() {
        axios.getAxios(this.props.token).get(api.configs.getClient.request_uri)
            .then((info) => { this.setState({ clients: info.data as Array<any> }); })
            .catch((e) =>  Notification.Service.pushError("getClient",e));
    }
    public render() {
        return (
            <ClientList clients={this.state.clients}/>
        );
    }

}