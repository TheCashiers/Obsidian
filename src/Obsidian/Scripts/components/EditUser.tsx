import * as React from "react"
import {UserForm} from "./UserForm"
export const EditUser = (props) => (
    <div className="modal">
        <div className="modal-dialog">
            <div className="modal-content">

                <div className="modal-header">
                    <h3>Editing</h3>
                    <button type="button" className="btn btn-default" data-dismiss="modal" aria-hidden="true">Close</button>
                </div>

                <div className="modal-body">
                    <UserForm onSubmit={props.onSubmit} onInputChange={props.onInputChange} username={props.username} password={props.password} action="Edit user"/>
                </div>
                
                <div className="modal-footer">
                    <button type="button" className="btn btn-primary">Save Changes</button>
                </div>

            </div>
        </div>
    </div>);