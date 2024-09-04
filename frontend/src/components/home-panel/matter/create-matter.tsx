import { Button } from "@/components/ui/button"
import {
    Dialog,
    DialogContent,
    DialogHeader,
    DialogTitle,
    DialogTrigger
} from "@/components/ui/dialog"
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from "@/components/ui/form"
import { Input } from "@/components/ui/input"
import { toast } from "@/components/ui/use-toast"
import { zodResolver } from "@hookform/resolvers/zod"
import { useForm } from "react-hook-form"
import { z } from "zod"

const CreateMatterFormSchema = z.object({
    name: z.string({ required_error: "Campo obrigatório" })
        .min(3, { message: 'Insira ao menos 3 caracteres' }),
})

export type CreateMatterFormInputs = z.infer<typeof CreateMatterFormSchema>

export function CreateMatter() {
    const form = useForm<CreateMatterFormInputs>({
        resolver: zodResolver(CreateMatterFormSchema),
        defaultValues: {
            name: "",
        },
    })

    function onSubmit(data: CreateMatterFormInputs) {
        toast({
          title: "You submitted the following values:",
            description: (
                <pre className="mt-2 w-[340px] rounded-md bg-slate-950 p-4">
                    <code className="text-white">{JSON.stringify(data, null, 2)}</code>
                </pre>
            ),
        })
      }

    return (
        <Dialog>
            <DialogTrigger asChild>
                <Button>
                    Adicionar
                </Button>
            </DialogTrigger>

            <DialogContent className="sm:max-w-[425px]">
                <DialogHeader>
                    <DialogTitle>Criar matéria</DialogTitle>
                </DialogHeader>
                <Form {...form}>
                    <form onSubmit={form.handleSubmit(onSubmit)} className="w-2/3 space-y-6">
                        <FormField
                            control={form.control}
                            name="name"
                            render={({ field }) => (
                                <FormItem>
                                    <FormLabel>Nome da matéria</FormLabel>
                                    <FormControl>
                                        <Input placeholder="Português" {...field} />
                                    </FormControl>
                                    <FormMessage />
                                </FormItem>
                            )}
                        />
                        <Button className="w-full" type="submit">Salvar</Button>
                    </form>
                </Form>
            </DialogContent>
        </Dialog>
    )
}
