import * as React from "react"
import {UserForm} from "./UserForm"
/*export const EditUser = (props) => (
    <div className="">
        <div className="">
            <div className="">

                <div className="">
                    <h3>Editing</h3>
                </div>

                <div className="content-wrapper well">
                    <UserForm onSubmit={props.onSubmit} onInputChange={props.onInputChange} username={props.username} password={props.password} action="Edit user"/>
                </div>
                

            </div>
        </div>
    </div>);
    */

export const EditUser = (props) => (
    <UserForm onSubmit={props.onSubmit} onInputChange={props.onInputChange} username={props.username} password={props.password} action="Edit User" isComplete={props.isComplete} isError={props.isError}/>

);