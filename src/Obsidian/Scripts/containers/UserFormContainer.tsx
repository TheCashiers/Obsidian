import * as React from "react";
import * as axios from "axios";
import * as api from "../configs/GlobalSettings";
import { UserForm } from "../components/UserForm";

interface IUserFormState {
    username?: string;
    password?: string;
}
interface IUserFormProps{
    action:string;
    target:api.IServerConfig;
}

export abstract class UserFormContainer extends React.Component<any, IUserFormState> {
    constructor(props: IUserFormProps) {
        super(props);
        this.state = { username: "", password: ""};
        this.handleInputChange = this.handleInputChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }


    handleInputChange(e) {
        this.setState({
            [e.target.name]: e.target.value as string
        });
    }
    handleSubmit(e) {
        e.preventDefault();
    }
    
    public render() {
        return (<UserForm
            onInputChange={this.handleInputChange}
            onSubmit={this.handleSubmit}
            username={this.state.username}
            password={this.state.password}
            action={this.props.action}/>);
    }
};

