import * as React from "react"
import {SignIn} from "../components/SignIn"
export class SignInContainer extends React.Component<any, any>
{
    constructor(props: any) {super(props);}
    public render(){
        return <SignIn/>
    }
}