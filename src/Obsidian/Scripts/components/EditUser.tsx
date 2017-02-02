import * as React from "react"
import {UserForm} from "./Form"

export const EditUser = (props) => (
    <UserForm onSubmit={props.onSubmit} 
        onInputChange={props.onInputChange} 
        username={props.username} 
        password={props.password} 
        action="Edit User"
    />
);