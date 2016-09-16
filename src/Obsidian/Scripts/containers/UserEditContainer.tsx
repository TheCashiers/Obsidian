import * as React from "react"
import { EditUser } from "../components/EditUser"
export class UserEditContainer extends React.Component<any, any>
{
    constructor(props: any) {
        super(props);
        console.log(props);
    }
    render(){
        return <EditUser username={this.props.location.query.username} id={this.props.location.query.id}/>
    }
}