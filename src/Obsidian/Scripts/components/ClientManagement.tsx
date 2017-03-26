import * as React from "react";
import { Link } from "react-router";
import { styles } from "../styles/index";
import { List } from "./List"

export const ClientList = (props) =>(
    <List createLink="/manage/clients/create"
        editLink="/manage/clients/edit"
        action="Create Client"
        items={props.clients}
        icon="fa-server"
    ></List>
)