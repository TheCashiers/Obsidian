import * as React from "react";

import { UserList } from "../components/UserManagement";
import * as axios from "axios";
import * as api from "../configs/GlobalSettings";
import { Main } from "../components/Global"




export class UserManagementContainer extends React.Component<api.IDefaultProps, any> {
    public render() {
        return (
            <Main>
                <UserList/>
            </Main>
        );
    }

}