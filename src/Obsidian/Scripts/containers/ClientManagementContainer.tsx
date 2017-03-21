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
    public async componentDidMount() {
        try {
            const response = await axios.getAxios(this.props.token).get(api.configs.getClient.request_uri)
            this.setState({ clients: response.data as Array<any> });
        } catch (error) {
            Notification.Service.pushError("getClient", error);
        }
    }
    public render() {
        return (
            <ClientList clients={this.state.clients}/>
        );
    }

}