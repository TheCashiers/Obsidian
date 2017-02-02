import * as React from "react"
import { UserFormContainer } from "./UserFormContainer"
import { ScopeForm } from "../components/Form";
import * as api from "../configs/GlobalSettings";
import * as axios from "axios";
import * as Notification from "./NotificationContainer"


export class ScopeEditContainer extends React.Component<any,any> {
    constructor(props) {
        super(props);
        this.state = { 
            id: props.location.query.id,
            scopeName:"",
            displayName:"",
            description:"",
            claimTypes:""
         };
    }
    public componentDidMount(){
        axios.get(api.configs.getScope.request_uri+"/"+this.state.id)
            .then((info) => {console.log(info); this.setState(info.data); })
            .catch((e) =>  Notification.Service.pushError("getClient",e));
    }
    handleInputChange(e) {
        this.setState({
            [e.target.name]: e.target.value as string
        });
    }
    handleSubmit(e) {
        e.preventDefault();
        let username: string = this.state.username.trim();
        let password: string = this.state.password.trim();
        if (username && password) {
            if (username)
                axios.put(`${api.configs.editUser.request_uri}${this.props.location.query.id}/UserName`, { username: username })
                    .then(() => {
                        Notification.Service.pushSuccess("Username changing");
                    })
                    .catch((e) => Notification.Service.pushError("Username changing", e));
            if (password != "") {
                axios.put(`${api.configs.editUser.request_uri}${this.props.location.query.id}/PassWord`, { password: password })
                    .then(() => {
                        Notification.Service.pushSuccess("Password changing");
                    })
                    .catch((e) => Notification.Service.pushError("Password changing", e));
            }
        } else { return; }
    }
    public render() {
        return (<ScopeForm
            onInputChange={this.handleInputChange}
            onSubmit={this.handleSubmit}
            scopeName={this.state.scopeName}
            displayName={this.state.displayName}
            description={this.state.description}
            claimTypes={this.state.claimTypes}
            action="Edit Scope" />);
    }
}