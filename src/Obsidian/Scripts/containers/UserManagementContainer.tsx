import * as React from "react";

import { UserList } from "../components/UserManagement";
import * as axios from "axios";
import * as SERVER_CONFIG from "../configs/GlobalSettings";
import { Main } from "../components/Global"


ReactDOM.render(
    <Main>
        <UserList/>
    </Main>, document.getElementById("usermanage")
);
