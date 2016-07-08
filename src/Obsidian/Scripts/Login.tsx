// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX

var Loginer = React.createClass({
    render: () =>{
        return (
            <div>
                Login to Obsidian
            </div>
        );
    }
});
ReactDOM.render(
    <Loginer />,
    document.getElementById('loginer')
);
