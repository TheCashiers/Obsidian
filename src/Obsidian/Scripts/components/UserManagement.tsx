// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
import * as React from "react";

const UserItem = (props) => (
    <li>
        {props.username}
    </li>
);

export const UserList = (props) => (
    <div className="content-wrapper">
        <h1>Users: </h1>
        <ul>
            {props.users.map((user, index) => <UserItem username={user.userName} key={user.id}/>) }
        </ul>
    </div>
)