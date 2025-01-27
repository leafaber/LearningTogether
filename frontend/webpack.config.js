module.exports = {
	devtool: 'source-map',
	entry: './src/index.jsx',
	mode: 'development',
	output: {
		filename: 'bundle.js',
		path: __dirname + '/dist'
	},
	resolve: {
		extensions: ['.ts', '.tsx', '.js', '.json', '.jsx', '.web.js', '.Webpack.js']
	},
	devServer: {
		static: {
			directory: './',
		},
		devMiddleware: {
			writeToDisk: true,
		},
		client: {
			progress: true,
		},
		compress: true,
		port: 3000,
	},
	watchOptions: {
		poll: 1000,
	},
	module: {
		rules: [
			{
				test: /\.(jsx)$/,
				exclude: /node_modules/,
				use: [{
					loader: 'babel-loader',
					options: {
						presets: [
							['@babel/preset-env', {
								"targets": "defaults"
							}],
							'@babel/preset-react'
						]
					}
				}]
			},
			{ 
				test: /\.tsx?$/,
				exclude: /(node_modules|bower_components)/,
				use: {
					loader: 'ts-loader'
				}
			},
			{
				test: /\.css$/i,
				use: ['style-loader', 'css-loader', 'postcss-loader']
			},
			{
				test: /\.svg/,
				type: 'asset/resource',
				generator: {
					filename: 'images/[hash][ext][query]'
				}
			},
			{
				test: /\.jpeg/,
				type: 'asset/resource',
				generator: {
					filename: 'images/[hash][ext][query]'
				}
			},
			{
				test: /\.png/,
				type: 'asset/resource',
				generator: {
					filename: 'images/[hash][ext][query]'
				}
			},
			{
				test: /\.jpg/,
				type: 'asset/resource',
				generator: {
					filename: 'images/[hash][ext][query]'
				}
			},
			{ 
				enforce: 'pre',
				test: /\.js$/,
				loader: 'source-map-loader'
			},
		]
	},

	externals: {
		'React': 'react',
		'ReactDOM': 'react-dom'
	}
};
