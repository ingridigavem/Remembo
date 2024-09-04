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
import { Icons } from "@/components/ui/icons";
import { Input } from "@/components/ui/input";
import { Popover, PopoverContent, PopoverTrigger } from "@/components/ui/popover";
import { Sheet, SheetContent, SheetDescription, SheetHeader, SheetTitle, SheetTrigger } from "@/components/ui/sheet";
import { Textarea } from "@/components/ui/textarea";
import { titleCase } from "@/lib/string";
import { cn } from "@/lib/utils";
import { createContent, fetchDashboard } from "@/redux/features/dashboard/thunk";
import { selectMatterOptions } from "@/redux/features/matters/mattersSlice";
import { createMatter } from "@/redux/features/matters/thunks";
import { useAppDispatch, useAppSelector } from "@/redux/hooks";
import { zodResolver } from "@hookform/resolvers/zod";
import { CaretSortIcon } from "@radix-ui/react-icons";
import { BookPlus, CheckIcon } from "lucide-react";
import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { z } from "zod";

const CreateReviewFormSchema = z.object({
    matterId: z.string({
        required_error: "Selecione uma matéria",
    }),
    name: z.string({
        required_error: "Insira o conteúdo",
    }),
    note: z.string().optional(),
})

export type CreateReviewFormInputs = z.infer<typeof CreateReviewFormSchema>

export function CreateReview() {
    const dispatch = useAppDispatch()
    const matters = useAppSelector(selectMatterOptions)
    const [search, setSearch] = useState("")
    const [open, setOpen] = useState(false)
    const [sheetOpen, setSheetOpen] = useState(false)

    const form = useForm<z.infer<typeof CreateReviewFormSchema>>({
        resolver: zodResolver(CreateReviewFormSchema),
    })

    async function handleAddMatter(name: string) {
        await dispatch(createMatter({ name }))
    }

    async function onSubmit(data: CreateReviewFormInputs) {
        await dispatch(createContent(data))
        await dispatch(fetchDashboard())
        form.reset()
        setSheetOpen(false)
    }

    useEffect(() => {
        const newMatter = matters.filter(m => m.label === search)
        if(newMatter.length > 0) {
            form.setValue("matterId", newMatter[0].value)
            setOpen(false)
        }
    }, [form, matters])


    const isLoading = form.formState.isSubmitting

    return (
        <Sheet open={sheetOpen} onOpenChange={setSheetOpen}>
            <SheetTrigger className="z-50 text-white fixed bottom-5 right-5 bg-green-500 rounded-full p-3 lg:bottom-10 lg:right-10">
                <BookPlus className="h-8 w-8" />
            </SheetTrigger>
            <SheetContent className="w-[400px] md:min-w-[540px]">
                <SheetHeader>
                    <SheetTitle>Criar revisão</SheetTitle>
                    <SheetDescription className="sr-only">Description goes here</SheetDescription>
                </SheetHeader>
                <div>
                    <Form {...form}>
                        <form onSubmit={form.handleSubmit(onSubmit)} className="pt-6 space-y-6">
                            <FormField
                                control={form.control}
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
                                                            {matters.map((matter) => (
                                                                <CommandItem
                                                                    value={matter.label}
                                                                    key={matter.value}
                                                                    onSelect={() => {
                                                                        form.setValue("matterId", matter.value)
                                                                    }}
                                                                >
                                                                    {titleCase(matter.label)}
                                                                        <CheckIcon
                                                                        className={cn(
                                                                            "ml-auto h-4 w-4",
                                                                            matter.value === field.value
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
                                {...form.register("matterId")}
                            />

                            <FormField
                                control={form.control}
                                render={({ field }) => (
                                    <FormItem>
                                        <FormLabel>Conteúdo</FormLabel>
                                        <FormControl>
                                            <Input {...field} />
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
                                {...form.register("note")}
                            />

                            <Button className="w-full" type="submit" disabled={isLoading}>
                                {isLoading && (
                                    <Icons.spinner className="mr-2 h-4 w-4 animate-spin" />
                                )}
                                Salvar
                            </Button>
                        </form>
                    </Form>
                </div>
            </SheetContent>
        </Sheet>
    )
}
