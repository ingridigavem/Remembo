import { createContext, useContext } from "react";

interface AuthContext {
  login: (token: string) => void;
  logout: () => void;
  isLoggedIn: () => boolean;
}

export const AuthContext = createContext({} as AuthContext)

export function AuthProvider({ children }: {children : React.ReactNode  }) {

  const isLoggedIn = () => {
    return !!localStorage.getItem(process.env.REACT_APP_TOKEN_KEY ?? "")
  }

  const login = (token: string) => {
    localStorage.setItem(process.env.REACT_APP_TOKEN_KEY ?? "", token)
  };

  const logout = () => {
    localStorage.setItem(process.env.REACT_APP_TOKEN_KEY ?? "", "")
  };

  return (
    <AuthContext.Provider
      value={{
        login,
        logout,
        isLoggedIn
      }}
    >
      {children}
    </AuthContext.Provider>
  )
}

export const useAuth = () => {
  return useContext(AuthContext);
}
