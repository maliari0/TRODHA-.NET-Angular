import { defineConfig } from 'vite';
import angular from '@analogjs/vite-plugin-angular';

export default defineConfig({
  plugins: [
    angular(),
  ],
  server: {
    proxy: {
      '/api': {
        target: 'http://localhost:5253',
        changeOrigin: true,
        secure: false,
        rewrite: (path) => path.replace(/^\/api/, '/api')
      }
    },
    port: 57438,
    https: true
  }
});
