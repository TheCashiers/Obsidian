// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX

import * as React from "react";
import * as ReactDOM from "react-dom";

//Server-side Config Here
const SERVER_DATA = {
    request_uri: "/api",
    invoke_func: "login"
};


export var LoginComponent = React.createClass({
    getInitialState: function () {
        return { username: '', password: '' };
    },
    handleUsernameChange: function (e) {
        this.setState({ username: e.target.value });
    },
    handlePasswordChange: function (e) {
        this.setState({ password: e.target.value });
    },
    handleSubmit: function (e) {
        e.preventDefault();
        var username = this.state.username.trim();
        var password = this.state.password.trim();
        if (!username || !password) {
            return;
        }
        // TODO: send request to the server
        console.log("login attempt:" + username + " // " + password);
        this.setState({ username: '', password: '' });
    },
    render: function () {
        return (
            <form onSubmit={this.handleSubmit}>
                <input
                    type="text"
                    placeholder="Username"
                    value={this.state.username}
                    onChange={this.handleUsernameChange}
                    />
                <input
                    type="password"
                    placeholder="Password"
                    value={this.state.password}
                    onChange={this.handlePasswordChange}
                    />
                <button type="submit">LOGIN</button>
            </form>
        );
    }
});