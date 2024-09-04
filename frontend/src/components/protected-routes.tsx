
import { selectUser } from "@/redux/features/user/userSlice";
import { useAppSelector } from "@/redux/hooks";
import { Navigate, Outlet } from "react-router-dom";

export const ProtectedRoutes = ({
    redirectPath = '/entrar',
    children,
  }: { redirectPath?: string, children?: React.ReactNode }) => {
    const user = useAppSelector(selectUser)

    if (!user) {
      return <Navigate to={redirectPath} replace />;
    }

    return children ? children : <Outlet />;
};
