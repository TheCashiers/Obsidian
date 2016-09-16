import * as React from "react";
import { Link } from "react-router";

const UserItem = (props) => (
    <li>
        {props.username}
        <Link to="/manage/users/edit/" query={{username:props.username,id:props.id}}>
            <button className="btn btn-lg btn-primary btn-raised">
                Edit
            </button>    
        </Link>
    </li>
);

export const UserList = (props) => (
    <div className="content-wrapper content">
        <div className="box box-solid box-info">
            <div className="box-header">
                <h3 className="box-title">Users</h3>
            </div>
            <div className="box-body">
            <ul>{props.users.map((user, index) => <UserItem username={user.userName} id={user.id} key={user.id}/>) }</ul>
            </div>
        </div>
        <Link to="/manage/users/create">
            <button className="btn btn-lg btn-success">Create User</button>
        </Link>
    </div>

);