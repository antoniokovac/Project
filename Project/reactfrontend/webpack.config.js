const path = require('path');

module.exports = {
    mode: 'development',
    entry: './src/app.js',
    output: {
        filename: 'bundle.js',
        path: path.join(__dirname, 'public')
    },
    module: {
        rules: [{
            loader: 'babel-loader',
            test: /\.js$/,
            exclude: path.resolve(__dirname, "node_modules")
        }],
    },
    devServer: {
        static: path.join(__dirname, 'public')
    }
}