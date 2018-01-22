import * as React from "react";
import { Link } from "react-router";
import { styles } from "../styles/index";
import { IListItem, List } from "./List";

export const ClientList = (props: {clients: IListItem[]}) => (
    <List
        createLink="/manage/clients/create"
        editLink="/manage/clients/edit"
        action="Create Client"
        items={props.clients}
        icon="fa-server"
    />
);
