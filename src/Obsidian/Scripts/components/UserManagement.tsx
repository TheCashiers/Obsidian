import * as React from "react";
import { Link } from "react-router";

const UserItem = (props) => (
    <div>
        <div className="list-group-item">
            <div className="row-action-primary">
                <i className="fa fa-user fa-2x"></i>
            </div>
            <div className="row-content">
                <div className="least-content">
                    <Link to="/manage/users/edit/" query={{ username: props.username, id: props.id }}>
                        <button className="btn btn-lg btn-primary btn-raised">
                            Edit
            </button>
                    </Link>
                </div>
                <h4 className="list-group-item-heading">{props.username}</h4>

                <p className="list-group-item-text">{props.id}</p>
            </div>
        </div>
        <div className="list-group-separator"></div>
    </div>
);
 
export const UserList = (props) => (
    <div className="content-wrapper content">
        <Link to="/manage/users/create">
            <button className="btn btn-primary btn-lg">Create User</button>
        </Link>
        
        <div className="list-group">
            {props.users.map((user, index) => <UserItem username={user.userName} id={user.id} key={user.id} />)}
        </div>
    </div>

);