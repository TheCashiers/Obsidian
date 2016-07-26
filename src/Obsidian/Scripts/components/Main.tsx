// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
import * as React from "react";

export let Main = React.createClass({
    render: function () {
        return (
            <div className="jumbotron">
                {this.props.children}
            </div>)
    }
    
});