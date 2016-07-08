// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
var Loginer = React.createClass({
    render: function () {
        return (React.createElement("div", null, "Login to Obsidian"));
    }
});
ReactDOM.render(React.createElement(Loginer, null), document.getElementById('loginer'));
//# sourceMappingURL=Login.js.map