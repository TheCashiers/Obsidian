import * as React from "react";
import { ScopeList } from "../components/ScopeManagement";
import * as axios from "../configs/AxiosInstance";
import * as api from "../configs/GlobalSettings";

export class ScopeManagementContainer extends React.Component<any, any> {
    constructor(props: any) {
        super(props);
        this.state = {
            scopes: [],
        };
        this.getScopes = this.getScopes.bind(this);
    }
    public async componentDidMount() {
        try {
            const response = await axios.getAxios(this.props.token).get(api.configs.getScope.request_uri);
            this.setState({ scopes: response.data as any[] });
        } catch (error) {
            this.props.push("getScope", error.toString());
        }
    }
    public getScopes() {
        return (this.state.scopes as any[]).filter((_, i) => (_.scopeName as string).includes(this.props.filter));
    }
    public render() {
        return (
            <ScopeList scopes={this.getScopes()}/>
        );
    }
}
