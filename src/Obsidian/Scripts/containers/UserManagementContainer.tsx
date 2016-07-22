// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
import * as React from "react";

import { UserList } from "../components/UserManagement";
import * as axios from "axios";

const SERVERSIDE_CONFIG = {
    request_uri: "/api/users",
    type: HTTP_TYPES.GET
}

enum HTTP_TYPES {
    GET,POST,PUT
}

