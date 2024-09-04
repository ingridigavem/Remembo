import { Button } from "@/components/ui/button";
import { Command, CommandEmpty, CommandGroup, CommandInput, CommandItem, CommandList } from "@/components/ui/command";
import {
    Form,
    FormControl,
    FormField,
    FormItem,
    FormLabel,
    FormMessage
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { Popover, PopoverContent, PopoverTrigger } from "@/components/ui/popover";
import { Sheet, SheetContent, SheetHeader, SheetTitle, SheetTrigger } from "@/components/ui/sheet";
import { Textarea } from "@/components/ui/textarea";
import { toast } from "@/components/ui/use-toast";
import { titleCase } from "@/lib/string";
import { cn } from "@/lib/utils";
import { zodResolver } from "@hookform/resolvers/zod";
import { CaretSortIcon } from "@radix-ui/react-icons";
import { BookPlus, CheckIcon } from "lucide-react";
import { useState } from "react";
import { useForm } from "react-hook-form";
import { z } from "zod";

const OPTIONS = [
    { label: "English", value: "en" },
    { label: "French", value: "fr" },
    { label: "German", value: "de" },
    { label: "Spanish", value: "es" },
    { label: "Russian", value: "ru" },
    { label: "Japanese", value: "ja" },
    { label: "Korean", value: "ko" },
    { label: "Chinese", value: "zh" },
]

const CreateReviewFormSchema = z.object({
    matter: z.string({
        required_error: "Selecione uma matéria",
    }),
    content: z.string({
        required_error: "Insira o conteúdo",
    }),
    note: z.string().optional(),
})

export type CreateReviewFormInputs = z.infer<typeof CreateReviewFormSchema>

export function CreateReview() {
    const [ matters, setMatters ] = useState(OPTIONS)
    const [search, setSearch] = useState("")
    const [open, setOpen] = useState(false)
    //const search = useCommandState((state) => state.search)

    const form = useForm<z.infer<typeof CreateReviewFormSchema>>({
        resolver: zodResolver(CreateReviewFormSchema),
    })

    function onSubmit(data: CreateReviewFormInputs) {
        toast({
            title: "You submitted the following values:",
            description: (
                <pre className="mt-2 w-[340px] rounded-md bg-slate-950 p-4">
                    <code className="text-white">{JSON.stringify(data, null, 2)}</code>
                </pre>
            ),
        })
    }

    function handleAddMatter(value: string) {
        setMatters([...matters, { label: titleCase(value), value: titleCase(value)}])
        form.setValue("matter", titleCase(value))
        setOpen(false)
    }

    return (
        <Sheet>
            <SheetTrigger className="z-50 fixed bottom-5 right-5 bg-green-500 rounded-full p-3 lg:bottom-10 lg:right-10">
                <BookPlus className="h-8 w-8" />
            </SheetTrigger>
            <SheetContent className="w-[400px] md:min-w-[540px]">
                <SheetHeader>
                    <SheetTitle>Criar revisão</SheetTitle>
                </SheetHeader>
                <div>
                    <Form {...form}>
                        <form onSubmit={form.handleSubmit(onSubmit)} className="pt-6 space-y-6">
                            <FormField
                                control={form.control}
                                name="matter"
                                render={({ field }) => (
                                <FormItem className="flex flex-col">
                                    <FormLabel>Matéria</FormLabel>
                                    <Popover open={open} onOpenChange={setOpen}>
                                        <PopoverTrigger asChild>
                                            <FormControl>
                                                <Button
                                                    variant="outline"
                                                    role="combobox"
                                                    className={cn(
                                                        "w-full justify-between",
                                                        !field.value && "text-muted-foreground"
                                                    )}
                                                >
                                                    {field.value ?
                                                        matters.find((language) => language.value === field.value)?.label
                                                        :
                                                        "Selecione uma matéria"
                                                    }
                                                    <CaretSortIcon className="ml-2 h-4 w-4 shrink-0 opacity-50" />
                                                </Button>
                                            </FormControl>
                                        </PopoverTrigger>
                                        <PopoverContent className="w-[380px] p-0">
                                            <Command>
                                                <CommandInput
                                                    placeholder="Pesquisar matéria..."
                                                    className="h-9"
                                                    value={search}
                                                    onValueChange={setSearch}
                                                />
                                                <CommandList>
                                                    <CommandEmpty>
                                                        <Button onClick={() => handleAddMatter(search)}>
                                                            Adicionar "{titleCase(search)}".
                                                        </Button>
                                                    </CommandEmpty>
                                                    <CommandGroup>
                                                        {matters.map((language) => (
                                                            <CommandItem
                                                                value={language.label}
                                                                key={language.value}
                                                                onSelect={() => {
                                                                    form.setValue("matter", language.value)
                                                                }}
                                                            >
                                                                {language.label}
                                                                    <CheckIcon
                                                                    className={cn(
                                                                        "ml-auto h-4 w-4",
                                                                        language.value === field.value
                                                                        ? "opacity-100"
                                                                        : "opacity-0"
                                                                    )}
                                                                />
                                                            </CommandItem>
                                                        ))}
                                                    </CommandGroup>
                                                </CommandList>
                                            </Command>
                                        </PopoverContent>
                                    </Popover>
                                    <FormMessage />
                                </FormItem>
                            )}
                            />

                            <FormField
                                control={form.control}
                                name="content"
                                render={({ field }) => (
                                    <FormItem>
                                        <FormLabel>Conteúdo</FormLabel>
                                        <FormControl>
                                            <Input {...field} />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />

                            <FormField
                                control={form.control}
                                name="note"
                                render={({ field }) => (
                                    <FormItem>
                                        <FormLabel>Observações</FormLabel>
                                        <FormControl>
                                            <Textarea
                                                rows={10}
                                                className="resize-none"
                                                {...field}
                                            />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />

                            <Button className="w-full" type="submit">Salvar</Button>
                        </form>
                    </Form>
                </div>
            </SheetContent>
      </Sheet>
    )
}
