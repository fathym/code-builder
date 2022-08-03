const path = require('path');

module.exports = {
  entry: './src/index.ts',
  module: {
    rules: [
      {
        test: /\.(js|jsx|ts|tsx)$/,
        exclude: /node_modules/,
        // use: {
        //   loader: 'ts-loader'
        // },
        use: {
          loader: 'babel-loader'
        },
      },
    ],
  },
  resolve: {
    extensions: ['.ts', '.js'],
  },
  output: {
    path: path.resolve(__dirname, '../wwwroot'),
    filename: 'jsPlumbInterop.js',
    library: 'FathymJSPlumbInterop',
  },
};
