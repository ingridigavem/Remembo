import { SignUpForm } from "@/components/autentication/signup-form";
import { Helmet } from "react-helmet-async";

export function SignUpPage() {
    return (
        <>
            <Helmet title="Cadastrar" />
            <SignUpForm />
        </>
    )
}
