import * as React from "react";
import * as axios from "axios";
import * as api from "../configs/GlobalSettings";
import { UserForm } from "../components/UserForm";

interface IUserCreationState {
    username?: string;
    password?: string;
    isComplete?: boolean;
    isError?:boolean;
}
interface IUserFormProps{
    action:string;
    target:api.IServerConfig;
}

export abstract class UserFormContainer extends React.Component<IUserFormProps, IUserCreationState> {
    constructor(props: IUserFormProps) {
        super(props);
        this.state = { username: "", password: "", isComplete: false ,isError:false};
        this.handleInputChange = this.handleInputChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }
    handleInputChange(e) {
        this.setState({
            [e.target.name]: e.target.value as string
        });
    }
    request(data:any){
        throw new Error("Not implemented request");
    }
    handleSubmit(e) {
        e.preventDefault();
        let username: string = this.state.username.trim();
        let password: string = this.state.password.trim();
        if (username && password) {
            this.request( { userName: username, password: password })
        } else { return; }
    }
    
    public render() {
        return (<UserForm
            onInputChange={this.handleInputChange}
            onSubmit={this.handleSubmit}
            username={this.state.username}
            password={this.state.password}
            isComplete={this.state.isComplete}
            isError={this.state.isError}
            action={this.props.action}/>);
    }
};

