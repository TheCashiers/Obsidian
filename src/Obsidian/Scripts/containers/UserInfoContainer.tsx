import * as React from "react";
import { UserInfo } from "../components/PortalElements";
import * as axios from 'axios';
import * as api from "../configs/GlobalSettings";

interface IUserInfoState {
    username: string;
    level: string;
    description: string;
}

export class UserInfoContainer extends React.Component<any, IUserInfoState>{
    constructor(props) {
        super(props);
        this.state = { description: "", level: "", username: "" };
    }
    public componentDidMount() {
        // todo: fetch some userinfo here;
        this.setState({ description: "A real boss.", level: "Administrator", username: "Henry Chu" });
        
    }
    public async handleSignout(){
        await axios.get(api.configs.signOut.request_uri)
        location.reload();
    }
    public render() {
        return (
                <UserInfo username={this.state.username} level={this.state.level} description={this.state.description} onSignout={this.handleSignout} />
        );
    }
}