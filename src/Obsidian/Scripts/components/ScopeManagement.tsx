import * as React from "react";
import { Link } from "react-router";
import { IScopeManagementState } from "../containers/ScopeManagementContainer";
import { List } from "./List";

export const ScopeList = (props: IScopeManagementState) => (
    <List
        createLink="/manage/scopes/create"
        editLink="/manage/scopes/edit"
        action="Create Scope"
        items={props.scopes}
        icon="fa-puzzle-piece"
    />
);
