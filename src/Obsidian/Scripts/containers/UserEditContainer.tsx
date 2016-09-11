import * as React from "react"
import { EditUser } from "../components/EditUser"
export class UserEditContainer extends React.Component<any, any>
{
    constructor(props: any) {super(props);}
    render(){
        return <EditUser/>
    }
}