// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX

export enum REQUEST_TYPES {
    GET,
    POST,
    PUT
}

export interface IServerConfig {
    request_uri: string,
    request_type: REQUEST_TYPES
}

export interface IDefaultProps {
    api: IServerConfig;
}

interface IServerConfigList{
    [id: string]: IServerConfig;
}
export const configs: IServerConfigList = {
    getUser: {
        request_uri: "/api/users/",
        request_type: REQUEST_TYPES.GET
    },
    getClient: {
        request_uri: "/api/clients/",
        request_type: REQUEST_TYPES.GET
    },
    getScope: {
        request_uri: "/api/scopes/",
        request_type: REQUEST_TYPES.GET
    },
    createUser: {
        request_type: REQUEST_TYPES.POST,
        request_uri: "/api/users/"
    },
    editUser:{
        request_type:REQUEST_TYPES.PUT,
        request_uri:"/api/users/"
    },
    createClient: {
        request_uri:"/api/clients/",
        request_type:REQUEST_TYPES.POST
    },
    editClient:{
        request_uri:"/api/clients/",
        request_type:REQUEST_TYPES.PUT
    },
    createScope:{
        request_uri:"/api/scopes/",
        request_type:REQUEST_TYPES.POST
    },
    editScope:{
        request_uri:"/api/scopes/",
        request_type:REQUEST_TYPES.PUT
    },
    signOut:{
        request_uri:"/manage/signout/",
        request_type:REQUEST_TYPES.GET
    }
}
