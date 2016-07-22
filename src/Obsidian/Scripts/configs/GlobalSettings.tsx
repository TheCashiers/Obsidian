// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
export const configs = {
    getUser: {
        request_uri: "/api/users",
        request_type: REQUEST_TYPES.GET
    }
}


enum REQUEST_TYPES {
    GET,
    POST,
    PUT
}