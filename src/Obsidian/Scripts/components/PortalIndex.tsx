// A '.tsx' file enables JSX support in the TypeScript compiler,
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
import * as React from "react";
import { Link } from "react-router";
import { styles } from "../styles/index";

export const PortalIndex = () => {
    return (
        <div style={styles.portalIndex} className="content-wrapper">
            <h1>Welcome to Obsidian Management Portal!</h1>
            <br />
            <h2>You can manage all the users, claims and clients here.</h2>
            <h2>Select what you need in the navigation bar.</h2>
        </div>
    );
};
