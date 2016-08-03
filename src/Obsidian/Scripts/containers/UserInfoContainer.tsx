import * as React from "react";
import { PortalHeader, UserInfo } from "../components/PortalElements"

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
        this.setState({ description: "A real boss.", level: "Administrator", username: "Henry Zhu" });
    }

    public render() {
        return (
            <PortalHeader>
                <UserInfo username={this.state.username} level={this.state.level} description={this.state.description} />
            </PortalHeader>
        );
    }
}