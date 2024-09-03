import { SignInForm } from "@/components/autentication/signin-form";
import { Helmet } from "react-helmet-async";

export function SignInPage() {
    return (
        <>
            <Helmet title="Entrar" />
            <SignInForm />
        </>
    )
}
