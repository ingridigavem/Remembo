import { createBrowserRouter } from "react-router-dom";
import { ProtectedRoutes } from "./components/protected-routes";
import { LayoutAuthentication } from "./pages/authentication/layout";
import { SignInPage } from "./pages/authentication/signin-page";
import { SignUpPage } from "./pages/authentication/signup-page";
import { HomePage } from "./pages/home/home-page";
import { LayoutHome } from "./pages/home/layout";
import { SubjectPage } from "./pages/home/subject-page";
import NotFoundPage from "./pages/not-found";

export const router = createBrowserRouter([
  {
    path: "",
    element: (
      <ProtectedRoutes>
        <LayoutHome />
      </ProtectedRoutes>
    ),
    children: [
      {
        path: "",
        element: <HomePage />,
      },
      {
        path: "/materias",
        element: <SubjectPage />,
      },
    ],
  },
  {
    path: "/",
    element: <LayoutAuthentication />,
    children: [
      {
        path: "/entrar",
        element: <SignInPage />,
      },
      {
        path: "/cadastrar",
        element: <SignUpPage />,
      },
    ],
  },
  {
    path: "*",
    element: <NotFoundPage />,
  },
]);
