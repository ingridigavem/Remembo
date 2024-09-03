import { Helmet, HelmetProvider } from 'react-helmet-async'
import { RouterProvider } from 'react-router-dom'
import { Toaster } from './components/ui/toaster'
import { AuthProvider } from './contexts/AuthContext'
import { ThemeProvider } from './contexts/ThemeContext'
import { router } from './routes'

function App() {
  return (
    <HelmetProvider>
      <Helmet titleTemplate="%s | Remembo" />
      <ThemeProvider defaultTheme="dark" storageKey="vite-ui-theme">
        <AuthProvider>
          <RouterProvider router={router} />
        </AuthProvider>
        <Toaster />
      </ThemeProvider>
    </HelmetProvider>
  )
}

export default App
