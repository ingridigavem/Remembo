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
import { fullNameValidation, onlyLettersValidation, passwordValidation } from "@/lib/validations";
import { Link } from "react-router-dom";
import { Card, CardContent } from "../ui/card";

const SignUpFormSchema = z.object({
    name: z.string({ required_error: "Campo obrigatório" })
        .min(1, { message: 'Insira ao menos 1 caracter' })
        .regex(onlyLettersValidation, { message: 'Deve conter apenas letras' })
        .regex(fullNameValidation, { message: 'Insira seu primeiro e ultimo nome' })
        .transform((name) => name.toUpperCase()),
    email: z.string({ required_error: "Campo obrigatório" })
        .min(1, { message: 'Insira ao menos 1 caracter' })
        .email("Insira um email válido"),
    password: z.string({ required_error: "Campo obrigatório" })
        .min(1, { message: 'Insira ao menos 1 caracter' })
        .regex(passwordValidation, { message: 'Sua senha não é valido'}),
})

export type SignUpFormInputs = z.infer<typeof SignUpFormSchema>

export function SignUpForm() {
    const form = useForm<SignUpFormInputs>({
        resolver: zodResolver(SignUpFormSchema),
    })

    function onSubmit(values: SignUpFormInputs) {
        console.log(values)
    }

    return (
        <div className="space-y-8 min-w-[480px]">
            <div className="sm:mx-auto sm:w-full sm:max-w-md">
                <img
                    alt="Remembo Icon"
                    src={Logo}
                    className="mx-auto h-14 w-auto"
                />
                <h2 className="mt-6 text-center text-2xl font-bold leading-9 tracking-tight">
                    Criar a sua conta
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
                                        <FormLabel>Nome completo</FormLabel>
                                        <FormControl>
                                            <Input placeholder="Seu nome" {...field} />
                                        </FormControl>
                                    <FormMessage />
                                    </FormItem>
                                )}
                                {...form.register("name")}
                            />

                            <FormField
                                control={form.control}
                                render={({ field }) => (
                                    <FormItem>
                                        <FormLabel>E-mail</FormLabel>
                                        <FormControl>
                                            <Input placeholder="exemplo@email.com" {...field} />
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
                                            <Input {...field} />
                                        </FormControl>
                                    <FormMessage />
                                    </FormItem>
                                )}
                            />
                            <Button type="submit" className="w-full">Cadastrar</Button>
                        </CardContent>
                    </Card>
                </form>
            </Form>

            <p className="mt-10 text-center text-sm text-gray-500">
                Já possui conta?{' '}
                <Link to="/cadastrar" className={buttonVariants({ variant: "link" })}>
                    Cadastre-se
                </Link>
            </p>
        </div>
    )
}
