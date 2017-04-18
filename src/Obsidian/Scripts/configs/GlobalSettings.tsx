// A '.tsx' file enables JSX support in the TypeScript compiler,
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX

export enum REQUEST_TYPES {
    GET,
    POST,
    PUT,
}

export interface IServerConfig {
    request_uri: string;
    request_type: REQUEST_TYPES;
}

export interface IDefaultProps {
    api: IServerConfig;
}

interface IServerConfigList {
    [id: string]: IServerConfig;
}
export const configs: IServerConfigList = {
    createClient: {
        request_type: REQUEST_TYPES.POST,
        request_uri: "/api/clients/",
    },
    createScope: {
        request_type: REQUEST_TYPES.POST,
        request_uri: "/api/scopes/",
    },
    createUser: {
        request_type: REQUEST_TYPES.POST,
        request_uri: "/api/users/",
    },
    editClient: {
        request_type: REQUEST_TYPES.PUT,
        request_uri: "/api/clients/",
    },
    editScope: {
        request_type: REQUEST_TYPES.PUT,
        request_uri: "/api/scopes/",
    },
    editUser: {
        request_type: REQUEST_TYPES.PUT,
        request_uri: "/api/users/",
    },
    getClient: {
        request_type: REQUEST_TYPES.GET,
        request_uri: "/api/clients/",
    },
    getScope: {
        request_type: REQUEST_TYPES.GET,
        request_uri: "/api/scopes/",
    },
    getUser: {
        request_type: REQUEST_TYPES.GET,
        request_uri: "/api/users/",
    },
    signOut: {
        request_type: REQUEST_TYPES.GET,
        request_uri: "/manage/signout/",
    },
};
