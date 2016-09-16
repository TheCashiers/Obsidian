import * as React from "react"
import {UserForm} from "./UserForm"
export const EditUser = (props) => (
    <div className="">
        <div className="">
            <div className="modal-content">

                <div className="">
                    <h3>Editing</h3>
                </div>

                <div className="">
                    <UserForm onSubmit={props.onSubmit} onInputChange={props.onInputChange} username={props.username} password={props.password} action="Edit user"/>
                </div>
                

            </div>
        </div>
    </div>);