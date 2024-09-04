import { Button } from "@/components/ui/button";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { titleCase } from "@/lib/string";
import { createSubject } from "@/redux/features/subjects/thunks";
import { useAppDispatch } from "@/redux/hooks";
import { zodResolver } from "@hookform/resolvers/zod";
import { useState } from "react";
import { useForm } from "react-hook-form";
import { z } from "zod";

const CreateSubjectFormSchema = z.object({
  name: z
    .string({ required_error: "Campo obrigatório" })
    .min(3, { message: "Insira ao menos 3 caracteres" })
    .transform((name) => titleCase(name)),
});

export type CreateSubjectFormInputs = z.infer<typeof CreateSubjectFormSchema>;

export function CreateSubject() {
  const [open, setOpen] = useState(false);
  const dispatch = useAppDispatch();

  const form = useForm<CreateSubjectFormInputs>({
    resolver: zodResolver(CreateSubjectFormSchema),
  });

  async function onSubmit(data: CreateSubjectFormInputs) {
    await dispatch(createSubject(data));
    form.reset();
    setOpen(false);
  }

  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogTrigger asChild>
        <Button>Adicionar</Button>
      </DialogTrigger>

      <DialogContent className="sm:max-w-[425px]">
        <DialogHeader>
          <DialogTitle>Criar matéria</DialogTitle>
        </DialogHeader>
        <Form {...form}>
          <form
            onSubmit={form.handleSubmit(onSubmit)}
            className="w-full space-y-6"
          >
            <FormField
              control={form.control}
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Nome da matéria</FormLabel>
                  <FormControl>
                    <Input placeholder="Português" {...field} />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
              {...form.register("name")}
            />
            <Button className="w-full" type="submit">
              Salvar
            </Button>
          </form>
        </Form>
      </DialogContent>
    </Dialog>
  );
}
