import * as React from "react";
import * as axios from "../configs/AxiosInstance";
import * as api from "../configs/GlobalSettings";
import { ScopeList } from "../components/ScopeManagement"

export class ScopeManagementContainer extends React.Component<any, any>
{
    constructor(props: any) {
        super(props);
        this.state = {
            scopes: []
        };
    }
    public async componentDidMount() {
        try {
            const response = await axios.getAxios(this.props.token).get(api.configs.getScope.request_uri);
            this.setState({ scopes: response.data as Array<any> });
        } catch (error) {
            this.props.push("getClient", error.toString())
        }
    }
    public render() {
        return (
            <ScopeList scopes={this.state.scopes}/>
        );
    }
}