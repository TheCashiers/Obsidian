// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
import * as React from "react";
import * as ReactDOM from "react-dom";

export var Main = React.createClass({
    render: () => (
        <div className="jumbotron">
            {this.props.children}
        </div>    
    )
});