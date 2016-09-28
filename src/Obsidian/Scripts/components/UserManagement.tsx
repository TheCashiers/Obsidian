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

        <div className="list-group-item">
            <div className="row-action-primary">
                <i className="fa fa-user"/>
            </div>
            <div className="row-content">
                <div className="action-secondary"><i className="fa fa-user"/></div>
                <h4 className="list-group-item-heading">Tile with an icon</h4>
                <p className="list-group-item-text">Donec id elit non mi porta gravida at eget metus.</p>
            </div>
        </div>
        <div className="list-group-separator"></div>



    
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