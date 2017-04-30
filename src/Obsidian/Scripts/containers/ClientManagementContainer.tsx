import * as React from "react";
import { ClientList } from "../components/ClientManagement";
import * as axios from "../configs/AxiosInstance";
import * as api from "../configs/GlobalSettings";

export class ClientManagementContainer extends React.Component<any, any> {
    constructor(props: any) {
        super(props);
        this.state = {
            clients: [],
        };
        this.getClients = this.getClients.bind(this);
    }
    public async componentDidMount() {
        try {
            const response = await axios.getAxios(this.props.token).get(api.configs.getClient.request_uri);
            this.setState({ clients: response.data as any[] });
        } catch (error) {
            this.props.push("getClient", error.toString());
        }
    }
    public getClients() {
        return (this.state.clients as any[]).filter((_, i) => (_.displayName as string).includes(this.props.filter));
    }
    public render() {
        return (
            <ClientList
                clients={this.getClients()}
            />
        );
    }

}
