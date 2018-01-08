import * as React from "react";
import { IListItem } from "../components/List";
import { ScopeList } from "../components/ScopeManagement";
import * as axios from "../configs/AxiosInstance";
import * as api from "../configs/GlobalSettings";
import { IFormProps } from "./FormContainer";

export interface IScopeManagementState {
    scopes: IListItem[];
}

export class ScopeManagementContainer extends React.Component<IFormProps, any> {
         constructor(props: IFormProps) {
           super(props);
           this.state = { scopes: [] };
           this.getScopes = this.getScopes.bind(this);
         }
         public async componentDidMount() {
           try {
             const response = await axios
               .getAxios(this.props.token)
               .get(api.configs.getScope.request_uri);
             this.setState({ scopes: response.data});
           } catch (error) {
             this.props.push("getScope", error.toString());
           }
         }
         public getScopes() {
           return (this.state.scopes).filter((_: IListItem, i: number) =>
             (_.scopeName as string).includes(this.props.filter),
           );
         }
         public render() {
           return <ScopeList scopes={this.getScopes()} />;
         }
       }
