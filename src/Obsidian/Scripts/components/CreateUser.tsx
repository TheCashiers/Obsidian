// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
import * as React from "react";
import { UserForm } from "./UserForm"

export const CreateUser = (props) => (
    <UserForm onSubmit={props.onSubmit} onInputChange={props.onInputChange} username={props.username} password={props.password} action="Create User"/>
);