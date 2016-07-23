﻿// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
export const configs = {
    getUser: {
        request_uri: "/api/users",
        request_type: REQUEST_TYPES.GET
    } as ServerConfig
}


enum REQUEST_TYPES {
    GET,
    POST,
    PUT
}

interface ServerConfig {
    request_uri: string,
    request_type: REQUEST_TYPES
}