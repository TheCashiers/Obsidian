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

export const configs = {
    getUser: {
        request_uri: "/api/users",
        request_type: REQUEST_TYPES.GET
    } as IServerConfig,
    createUser: {
        request_type: REQUEST_TYPES.POST,
        request_uri: "/api/users"
    } as IServerConfig
}