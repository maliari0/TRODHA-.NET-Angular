// trodha.client/src/proxy.conf.js
const { env } = require('process');

// Hedef API URL'ini belirle
const target = 'http://localhost:5253';

const PROXY_CONFIG = [
  {
    context: [
      "/api",
    ],
    target,
    secure: false,
    logLevel: "debug",
    changeOrigin: true,
    pathRewrite: {
      "^/api": "/api"
    }
  }
]

module.exports = PROXY_CONFIG;
