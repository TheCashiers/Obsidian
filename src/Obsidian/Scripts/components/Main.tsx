// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
import * as React from "react";
import { PortalHeader, PortalSidebar } from "./PortalElements"
export let Main = React.createClass({
    render: function () {
        return (
            <div className="wrapper skin-blue">
                <PortalHeader/>
                <PortalSidebar/>
                {this.props.children}
            </div>)
    }
});