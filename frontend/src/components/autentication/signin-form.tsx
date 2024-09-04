import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { z } from "zod";

import Logo from "@/assets/logo128.png";
import { Button, buttonVariants } from "@/components/ui/button";
import {
    Form,
    FormControl,
    FormField,
    FormItem,
    FormLabel,
    FormMessage
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import api, { ResponseApi } from "@/lib/api";
import { login } from "@/redux/features/user/userSlice";
import { useAppDispatch } from "@/redux/hooks";
import { AxiosResponse } from "axios";
import { Link, useNavigate } from "react-router-dom";
import { Card, CardContent } from "../ui/card";
import { Icons } from "../ui/icons";

const SignInFormSchema = z.object({
    email: z.string({ required_error: "Campo obrigatório" })
        .min(1, { message: 'Insira ao menos 1 caracter' })
        .email("Insira um email válido"),
    password: z.string({ required_error: "Campo obrigatório" })
        .min(1, { message: 'Insira ao menos 1 caracter' })
        //.regex(passwordValidation, { message: 'Sua senha não é valido' }),
})

export type SignInFormInputs = z.infer<typeof SignInFormSchema>

export function SignInForm() {
    const navigate = useNavigate();
    const dispatch = useAppDispatch();

    const form = useForm<SignInFormInputs>({
        resolver: zodResolver(SignInFormSchema),
    })

    const isLoading = form.formState.isSubmitting

    function onSubmit(values: SignInFormInputs) {
        api.post("/api/account/login", values).then((response: AxiosResponse<ResponseApi<string>>) => {
            if(response.data.success) {
                dispatch(login(response.data.data))
                navigate("/")
            }
        }).catch((e) => {
            console.log(e)
        })
    }

    return (
        <div className="space-y-8 min-w-[480px]">
            <div className="sm:mx-auto sm:w-full sm:max-w-md">
                <div className="space-y-2">
                    <img
                        alt="Remembo Icon"
                        src={Logo}
                        className="mx-auto h-14 w-auto"
                    />
                    <h1 className="text-center text-2xl font-bold uppercase font-brand text-primary tracking-widest">Remembo</h1>
                </div>
                <h2 className="mt-6 text-center text-2xl font-bold leading-9 tracking-tight">
                    Entrar na sua conta
                </h2>
            </div>


            <Form {...form}>
                <form onSubmit={form.handleSubmit(onSubmit)} >
                    <Card className="px-6 py-12 shadow sm:px-12">
                        <CardContent className="space-y-6">
                            <FormField
                                control={form.control}
                                render={({ field }) => (
                                    <FormItem>
                                        <FormLabel>E-mail</FormLabel>
                                        <FormControl>
                                            <Input placeholder="exemplo@email.com" disabled={isLoading} {...field} />
                                        </FormControl>
                                    <FormMessage />
                                    </FormItem>
                                )}
                                {...form.register("email")}
                            />
                            <FormField
                                control={form.control}
                                {...form.register("password")}
                                render={({ field }) => (
                                    <FormItem>
                                        <FormLabel>Senha</FormLabel>
                                        <FormControl>
                                            <Input type="password" disabled={isLoading} {...field} />
                                        </FormControl>
                                    <FormMessage />
                                    </FormItem>
                                )}
                            />
                            <Button type="submit" className="w-full" disabled={isLoading}>
                                {isLoading && (
                                    <Icons.spinner className="mr-2 h-4 w-4 animate-spin" />
                                )}
                                Entrar
                            </Button>
                        </CardContent>
                    </Card>
                </form>
            </Form>

            <p className="mt-10 text-center text-sm text-gray-500">
                Ainda não possui conta?
                <Link to="/cadastrar" className={buttonVariants({ variant: "link" })}>
                    Cadastre-se
                </Link>
            </p>
        </div>
    )
}
