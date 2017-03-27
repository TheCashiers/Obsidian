var webpack = require('webpack');
var HappyPack = require('happypack');
var path = require('path');
const ExtractTextPlugin = require("extract-text-webpack-plugin");

module.exports = {
    entry: {
        app: "./Scripts/UserPortalPage.tsx",
        vendor: ["react", "react-dom", "react-router","snackbarjs","jquery","axios"],
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
                    use: path.resolve(__dirname, './node_modules/happypack/loader')+"?id=css"
                })
            },
            {
                test: /\.tsx?$/,
                loader: "ts-loader",
                include: path.resolve(__dirname,"./Scripts")
            },
            {
                test: /\.(ttf|otf|eot|svg|woff(2)?)(\?[a-z0-9]+)?$/,
                loader: 'file-loader?name=[name].[ext]&publicPath=fonts/&outputPath=wwwroot/lib/fonts/'
            }
        ]
    },
    plugins: [
        new webpack.PrefetchPlugin("axios/index.js"),
        new webpack.optimize.CommonsChunkPlugin({
            name: ['vendor','styles']
        }),
        new ExtractTextPlugin("./wwwroot/lib/styles.css"),
        new webpack.ProvidePlugin({
            $: "jquery",
            jQuery: "jquery"
        }),
        new HappyPack({
            id: "css",
            loaders:["css-loader"]
        })
    ],
};