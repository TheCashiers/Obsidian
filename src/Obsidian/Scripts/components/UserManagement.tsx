import * as React from "react";
import { Link } from "react-router";
import { IUserManagementState } from "../containers/UserManagementContainer";
import { List } from "./List";

export const UserList = (props: IUserManagementState) => (
    <List
        createLink="/manage/users/create"
        editLink="/manage/users/edit"
        action="Create User"
        items={props.users}
        icon="fa-user"
    />
);
