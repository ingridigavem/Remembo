
import { useAuth } from "@/contexts/AuthContext";
import { Navigate, Outlet } from "react-router-dom";

export const ProtectedRoutes = ({
    redirectPath = '/entrar',
    children,
  }: { redirectPath?: string, children?: React.ReactNode }) => {
    const { isLoggedIn } = useAuth();
    console.log(isLoggedIn)
    if (!isLoggedIn()) {
      return <Navigate to={redirectPath} replace />;
    }

    return children ? children : <Outlet />;
};
