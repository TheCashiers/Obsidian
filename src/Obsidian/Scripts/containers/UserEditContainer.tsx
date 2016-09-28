import * as React from "react"
import { UserFormContainer } from "./UserFormContainer"
import { EditUser } from "../components/EditUser";
import * as api from "../configs/GlobalSettings";
import * as axios from "axios";


export class UserEditContainer extends UserFormContainer
{
    constructor(props) {
        super(props);
        this.state={ username:props.location.query.username, password: "protectedContext", isComplete: false ,isError:false};
    }
    handleSubmit(e) {
        e.preventDefault();
        console.log(this.props);
        let username: string = this.state.username.trim();
        let password: string = this.state.password.trim();
        if (username && password) {
            axios.put(`${api.configs.editUser.request_uri}${this.props.location.query.id}/UserName`, username)
                .then(()=>{
                    this.setState({ isComplete: true });
                })
                .catch((e) => this.setState({ isError: true }));
        } else { return; }
    }
    public render() {
        return (<EditUser
            onInputChange={this.handleInputChange}
            onSubmit={this.handleSubmit}
            username={this.state.username}
            password={this.state.password}
            isComplete={this.state.isComplete}
            isError={this.state.isError}
            action={this.props.action}/>);
    }
}