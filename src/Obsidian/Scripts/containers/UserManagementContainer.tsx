import * as React from "react";

import { UserList } from "../components/UserManagement";
import * as axios from "axios";
import * as SERVER_CONFIG from "../configs/GlobalSettings";
import { Main } from "../components/Global"



export let UserControlContainer = React.createClass({
    render: function () {
        return (
            <Main>
                <UserList/>
            </Main>
        )
    }
});