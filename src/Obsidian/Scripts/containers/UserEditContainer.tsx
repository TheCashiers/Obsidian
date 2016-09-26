import * as React from "react"
import { EditUser } from "../components/EditUser"
import {UserFormContainer} from "./UserFormContainer"
export class UserEditContainer extends UserFormContainer
{
    constructor(props) {
        super(props);
    }
    request(data:any){
    }
}