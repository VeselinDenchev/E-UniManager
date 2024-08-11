import { defineConfig, loadEnv  } from 'vite'
import react from '@vitejs/plugin-react'

// https://vitejs.dev/config/
export default defineConfig(({ mode }) => {
  const env = loadEnv(mode, process.cwd())
  const apiUrl = new URL(env.VITE_CLIENT_BASE_URL);

  return {
    plugins: [react()],
    server: {
      host: true,
      strictPort: true,
      port: apiUrl.port
    }
  };
})
