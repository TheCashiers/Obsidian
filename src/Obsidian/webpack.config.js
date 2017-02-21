var webpack = require('webpack');
var path = require('path');
const ExtractTextPlugin = require("extract-text-webpack-plugin");

module.exports = {
    entry: {
        app: "./Scripts/UserPortalPage.tsx",
        vendor: ["react", "react-dom", "react-router","snackbarjs","jquery"],
        styles: ["bootstrap","bootstrap-material-design","./Scripts/styles/vendor.js"]
    },
    output: {
        filename: "./wwwroot/js/[name].bundle.js",
    },
    resolve: {
        extensions: [".webpack.js", ".web.js", ".ts", ".tsx", ".js", ".css"]
    },
    module: {
        rules: [
            {
                test: /\.css$/,
                use: ExtractTextPlugin.extract({
                    fallback: "style-loader",
                    use: "css-loader"
                })
            },
            { test: /\.tsx?$/, loader: "ts-loader" },
            {
                test: /\.(ttf|otf|eot|svg|woff(2)?)(\?[a-z0-9]+)?$/,
            },
            { test: /\.js$/, loader: "source-map-loader", enforce: 'pre' }
        ]
    },
    plugins: [
        new webpack.optimize.CommonsChunkPlugin({
            name: ['vendor','styles']
        }),
        new ExtractTextPlugin("./wwwroot/lib/styles.css"),
        new webpack.ProvidePlugin({
            $: "jquery",
            jQuery: "jquery"
        })
    ],
};